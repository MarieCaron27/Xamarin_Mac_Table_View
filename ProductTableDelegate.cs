namespace vues_table;

public class ProductTableDelegate : NSTableViewDelegate
{
    #region Constants
    private const string CellIdentifier = "ProdCell";
    #endregion

    #region Private Variables
    private ProductTableDataSource DataSource;
    private ViewController Controller;
    #endregion

    #region Constructors
    public ProductTableDelegate (ViewController controller, ProductTableDataSource datasource)
    {
      this.Controller = controller;
      this.DataSource = datasource;
    }
    #endregion

    #region Override Methods
    /*public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row)
    {
      // This pattern allows you reuse existing views when they are no-longer in use.
      // If the returned view is null, you instance up a new view
      // If a non-null view is returned, you modify it enough to reflect the new data
      NSTextField view = (NSTextField)tableView.MakeView (CellIdentifier, this);
      if (view == null) {
        view = new NSTextField ();
        view.Identifier = CellIdentifier;
        view.BackgroundColor = NSColor.Clear;
        view.Bordered = false;
        view.Selectable = false;
        view.Editable = false;
      }

      // Setup view based on the column selected
      switch (tableColumn.Title) {
      case "Product":
        view.StringValue = DataSource.Products [(int)row].Title;
        break;
      case "Details":
        view.StringValue = DataSource.Products [(int)row].Description;
        break;
      }

      return view;
    }*/
    
    /*public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row) //Allows the user to edit the table view
    {
      // This pattern allows you reuse existing views when they are no-longer in use.
      // If the returned view is null, you instance up a new view
      // If a non-null view is returned, you modify it enough to reflect the new data
      NSTextField view = (NSTextField)tableView.MakeView (tableColumn.Title, this);
      if (view == null) {
        view = new NSTextField ();
        view.Identifier = tableColumn.Title;
        view.BackgroundColor = NSColor.Clear;
        view.Bordered = false;
        view.Selectable = false;
        view.Editable = true;

        view.EditingEnded += (sender, e) => {

          // Take action based on type
          switch(view.Identifier) {
            case "Product":
              DataSource.Products [(int)view.Tag].Title = view.StringValue;
              break;
            case "Details":
              DataSource.Products [(int)view.Tag].Description = view.StringValue;
              break;
          }
        };
      }

      // Tag view
      view.Tag = row;

      // Setup view based on the column selected
      switch (tableColumn.Title) {
        case "Product":
          view.StringValue = DataSource.Products [(int)row].Title;
          break;
        case "Details":
          view.StringValue = DataSource.Products [(int)row].Description;
          break;
      }

      return view;
    }*/
    
    /*public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row) //Allows image adding
    {

      // This pattern allows you reuse existing views when they are no-longer in use.
      // If the returned view is null, you instance up a new view
      // If a non-null view is returned, you modify it enough to reflect the new data
      NSTableCellView view = (NSTableCellView)tableView.MakeView (tableColumn.Title, this);
      if (view == null) {
        view = new NSTableCellView ();
        if (tableColumn.Title == "Product") {
          view.ImageView = new NSImageView (new CGRect (0, 0, 16, 16)); //CGRect = Modifies the current graphics context clipping path by intersecting it with this rect
          view.AddSubview (view.ImageView);
          view.TextField = new NSTextField (new CGRect (20, 0, 400, 16));
        } else {
          view.TextField = new NSTextField (new CGRect (0, 0, 400, 16));
        }
        view.TextField.AutoresizingMask = NSViewResizingMask.WidthSizable;
        view.AddSubview (view.TextField);
        view.Identifier = tableColumn.Title;
        view.TextField.BackgroundColor = NSColor.Clear;
        view.TextField.Bordered = false;
        view.TextField.Selectable = false;
        view.TextField.Editable = true;

        view.TextField.EditingEnded += (sender, e) => {

          // Take action based on type
          switch(view.Identifier) {
            case "Product":
              DataSource.Products [(int)view.TextField.Tag].Title = view.TextField.StringValue;
              break;
            case "Details":
              DataSource.Products [(int)view.TextField.Tag].Description = view.TextField.StringValue;
              break;
          }
        };
      }

      // Tag view
      view.TextField.Tag = row;

      // Setup view based on the column selected
      switch (tableColumn.Title) {
        case "Product":
          view.ImageView.Image = NSImage.ImageNamed ("tags.png");
          view.TextField.StringValue = DataSource.Products [(int)row].Title;
          break;
        case "Details":
          view.TextField.StringValue = DataSource.Products [(int)row].Description;
          break;
      }

      return view;
    }*/
    
    public override NSView GetViewForItem (NSTableView tableView, NSTableColumn tableColumn, nint row)
    {

      // This pattern allows you reuse existing views when they are no-longer in use.
      // If the returned view is null, you instance up a new view
      // If a non-null view is returned, you modify it enough to reflect the new data
      NSTableCellView view = (NSTableCellView)tableView.MakeView (tableColumn.Title, this);
      if (view == null) {
        view = new NSTableCellView ();

        // Configure the view
        view.Identifier = tableColumn.Title;

        // Take action based on title
        switch (tableColumn.Title) {
        case "Product":
          view.ImageView = new NSImageView (new CGRect (0, 0, 16, 16));
          view.AddSubview (view.ImageView);
          view.TextField = new NSTextField (new CGRect (20, 0, 400, 16));
          ConfigureTextField (view, row);
          break;
        case "Details":
          view.TextField = new NSTextField (new CGRect (0, 0, 400, 16));
          ConfigureTextField (view, row);
          break;
        case "Action":
          // Create new button
          var button = new NSButton (new CGRect (0, 0, 81, 16));
          button.SetButtonType (NSButtonType.MomentaryPushIn);
          button.Title = "Delete";
          button.Tag = row;

          // Wireup events
          button.Activated += (sender, e) => {
            // Get button and product
            var btn = sender as NSButton;
            var product = DataSource.Products [(int)btn.Tag];

            // Configure alert
            var alert = new NSAlert () {
              AlertStyle = NSAlertStyle.Informational,
              InformativeText = $"Are you sure you want to delete {product.Title}? This operation cannot be undone.",
              MessageText = $"Delete {product.Title}?",
            };
            alert.AddButton ("Cancel");
            alert.AddButton ("Delete");
            alert.BeginSheetForResponse (Controller.View.Window, (result) => {
              // Should we delete the requested row?
              if (result == 1001) {
                // Remove the given row from the dataset
                DataSource.Products.RemoveAt((int)btn.Tag);
                Controller.ReloadTable ();
              }
            });
          };

          // Add to view
          view.AddSubview (button);
          break;
        }

      }

      // Setup view based on the column selected
      switch (tableColumn.Title) {
      case "Product":
        view.ImageView.Image = NSImage.ImageNamed ("test3.jpeg");
        view.TextField.StringValue = DataSource.Products [(int)row].Title;
        view.TextField.Tag = row;
        break;
      case "Details":
        view.TextField.StringValue = DataSource.Products [(int)row].Description;
        view.TextField.Tag = row;
        break;
      case "Action":
        foreach (NSView subview in view.Subviews) {
          var btn = subview as NSButton;
          if (btn != null) {
            btn.Tag = row;
          }
        }
        break;
      }

      return view;
    }
    #endregion

    #region One line selection
    public override bool ShouldSelectRow (NSTableView tableView, nint row)
    {
      return true;
    }
    #endregion
    
    #region Reorder columns
    public override bool ShouldReorder (NSTableView tableView, nint columnIndex, nint newColumnIndex)
    {
      return true;
    }
    #endregion

    #region Caracter matches the first line of Product or Details
    public override nint GetNextTypeSelectMatch (NSTableView tableView, nint startRow, nint endRow, string searchString)
    {
      nint row = 0;
      foreach(Product product in DataSource.Products) {
        if (product.Title.Contains(searchString)) return row;

        // Increment row counter
        ++row;
      }

      // If not found select the first row
      return 0;
    }
    #endregion
    
    #region Configure TxtField 
    private void ConfigureTextField (NSTableCellView view, nint row)
    {
      // Add to view
      view.TextField.AutoresizingMask = NSViewResizingMask.WidthSizable;
      view.AddSubview (view.TextField);

      // Configure
      view.TextField.BackgroundColor = NSColor.Clear;
      view.TextField.Bordered = false;
      view.TextField.Selectable = false;
      view.TextField.Editable = true;

      // Wireup events
      view.TextField.EditingEnded += (sender, e) => {

        // Take action based on type
        switch (view.Identifier) {
          case "Product":
            DataSource.Products [(int)view.TextField.Tag].Title = view.TextField.StringValue;
            break;
          case "Details":
            DataSource.Products [(int)view.TextField.Tag].Description = view.TextField.StringValue;
            break;
        }
      };

      // Tag view
      view.TextField.Tag = row;
    }
    #endregion
}