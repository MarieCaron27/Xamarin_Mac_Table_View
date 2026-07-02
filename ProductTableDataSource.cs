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
}