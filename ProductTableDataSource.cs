using AppKit;

namespace vues_table;

public class ProductTableDataSource : NSTableViewDataSource
{
    #region Public Variables
    public List<Product> Products = new ();
    #endregion

    #region Constructors
    public ProductTableDataSource ()
    {
    }
    #endregion

    #region Override Methods
    public override nint GetRowCount (NSTableView tableView)
    {
        return Products.Count;
    }
    #endregion
    
    #region Sort Methods
    public void Sort(string key, bool ascending) {

        // Take action based on key
        switch (key) {
            case "Title":
                if (ascending) {
                    Products.Sort ((x, y) => x.Title.CompareTo (y.Title));
                } else {
                    Products.Sort ((x, y) => -1 * x.Title.CompareTo (y.Title));
                }
                break;
            case "Description":
                if (ascending) {
                    Products.Sort ((x, y) => x.Description.CompareTo (y.Description));
                } else {
                    Products.Sort ((x, y) => -1 * x.Description.CompareTo (y.Description));
                }
                break;
        }

    }

    public override void SortDescriptorsChanged (NSTableView tableView, NSSortDescriptor[] oldDescriptors)
    {
        // Sort the data => On passe ici au deuxième clique sur Product ou Details
        if (oldDescriptors.Length > 0) {
            // Update sort
            Sort (oldDescriptors [0].Key, oldDescriptors [0].Ascending);
        } else {
            // Grab current descriptors and update sort => On passe par ici lors du premier clique sur Product ou Details
            NSSortDescriptor[] tbSort = tableView.SortDescriptors;
            Sort (tbSort[0].Key, tbSort[0].Ascending);
        }

        // Refresh table
        tableView.ReloadData ();
    }
    #endregion
}