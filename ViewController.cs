using ObjCRuntime;

namespace vues_table;

public partial class ViewController : NSViewController
{
    protected ViewController(NativeHandle handle) : base(handle)
    {
        // This constructor is required if the view controller is loaded from a xib or a storyboard.
        // Do not put any initialization here, use ViewDidLoad instead.
    }

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        // Do any additional setup after loading the view.
    }

    public override NSObject RepresentedObject
    {
        get => base.RepresentedObject;
        set
        {
            base.RepresentedObject = value;

            // Update the view, if already loaded.
        }
    }
    
    public override void AwakeFromNib ()
    {
        base.AwakeFromNib ();

        // Create the Product Table Data Source and populate it
        var DataSource = new ProductTableDataSource ();
        DataSource.Products.Add (new Product ("Xamarin.iOS", "Allows you to develop native iOS Applications in C#"));
        DataSource.Products.Add (new Product ("Xamarin.Android", "Allows you to develop native Android Applications in C#"));
        DataSource.Products.Add (new Product ("Xamarin.Mac", "Allows you to develop Mac native Applications in C#"));

        // Populate the Product Table
        ProductTable.DataSource = DataSource;
        ProductTable.Delegate = new ProductTableDelegate (DataSource);
    }
}