using ConsignmentShopLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsignmentShopUI
{
    public partial class ConsignmentShop : Form
    {
        private Store store = new Store();
        private List<Item> shoppingCartData = new List<Item>();
        BindingSource itemsBinding = new BindingSource();
        BindingSource shoppingCartBinding = new BindingSource();

        public ConsignmentShop()
        {
            InitializeComponent();
            SetupData();

            // BIND
            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList(); // lambda expression filter --> for every item in list(x)  where x.sold = false
            itemsListBox.DataSource = itemsBinding;

            // DISPLAY
            itemsListBox.DisplayMember = "Display"; // display property from Item.cs
            itemsListBox.ValueMember = "Display";

            shoppingCartBinding.DataSource = shoppingCartData;
            shoppingCartListBox.DataSource = shoppingCartBinding;

            shoppingCartListBox.DisplayMember = "Display";
            shoppingCartListBox.ValueMember = "Display";
        }

        private void ConsignmentShop_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SetupData()
        {
            //Vendor demoVendor = new Vendor();

            //demoVendor.FirstName = "Bill";
            //demoVendor.LastName = "Smith";
            //demoVendor.Commission = .5;

            //store.Vendors.Add(demoVendor);

            //demoVendor = new Vendor();

            //demoVendor.FirstName = "Sue";
            //demoVendor.LastName = "Jones";
            //demoVendor.Commission = .5;

            //store.Vendors.Add(demoVendor);

            store.Vendors.Add(new Vendor { FirstName = "Bill", LastName = "O'Really" });
            store.Vendors.Add(new Vendor { FirstName = "Sue", LastName = "Jones" });
            store.Vendors.Add(new Vendor { FirstName = "Jack", LastName = "Bauer", Commission = 0.6 }); //if you want to override the default

            store.Items.Add(new Item
            {
                Title = "Moby Dick",
                Description = "The quest is an obsession and the novel is a diabolical study of how a man becomes a fanatic. But it is also a hymn to democracy. Bent as the crew is on Ahab s appalling crusade, it is equally the image of a co-operative community at work: all hands dependent on all hands, each individual responsible for the security of each. Among the crew is Ishmael, the novel's narrator, ordinary sailor, and extraordinary reader. Digressive, allusive, vulgar, transcendent, the story Ishmael tells is above all an education: in the practice of whaling, in the art of writing.",
                Price = 3.39M, // 'M' suffix to create a literal of this type
                Owner = store.Vendors[0]
            });

            store.Items.Add(new Item
            {
                Title = "The Fault In Our Stars",
                Description = "Despite the tumor-shrinking medical miracle that has bought her a few years, Hazel has never been anything but terminal, her final chapter inscribed upon diagnosis. But when a gorgeous plot twist named Augustus Waters suddenly appears at Cancer Kid Support Group, Hazel’s story is about to be completely rewritten.",
                Price = 8.69M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Title = "The Underground Railroad",
                Description = "Cora is a slave on a cotton plantation in Georgia. Life is hell for all the slaves, but especially bad for Cora; an outcast even among her fellow Africans, she is coming into womanhood—where even greater pain awaits. When Caesar, a recent arrival from Virginia, tells her about the Underground Railroad, they decide to take a terrifying risk and escape. Matters do not go as planned—Cora kills a young white boy who tries to capture her. Though they manage to find a station and head north, they are being hunted.In Whitehead’s ingenious conception, the Underground Railroad is no mere metaphor—engineers and conductors operate a secret network of tracks and tunnels beneath the Southern soil.Cora and Caesar’s first stop is South Carolina, in a city that initially seems like a haven.But the city’s placid surface masks an insidious scheme designed for its black denizens.And even worse: Ridgeway, the relentless slave catcher, is close on their heels.Forced to flee again, Cora embarks on a harrowing flight, state by state, seeking true freedom.",
                Price = 17.90M,
                Owner = store.Vendors[1]
            });

            store.Items.Add(new Item
            {
                Title = "The Tailor of Panama",
                Description = "He is Harry Pendel: Exclusive tailor to Panama’s most powerful men. Informant to British Intelligence. The perfect spy in a country rife with corruption and revolution. What his “handlers” don’t realize is that Harry has a hidden agenda of his own. Deceiving his friends, his wife, and practically himself, he’ll weave a plot so fabulous it exceeds his own vivid imagination. But when events start to spin out of control, Harry is suddenly in over his head—thrown into a lethal maze of politics and espionage, with unthinkable consequences...",
                Price = 13.59M,
                Owner = store.Vendors[2]
            });

            store.Name = "Second to Last";
        }

        private void addToCart_Click(object sender, EventArgs e)
        {
            // Figure out what is selected from the items list
            Item selectedItem = (Item)itemsListBox.SelectedItem;

            // Copy item to shopping cart list
            shoppingCartData.Add(selectedItem);

            shoppingCartBinding.ResetBindings(false);
        }

        private void makePurchase_Click(object sender, EventArgs e)
        {
            // Mark each item in the cart as sold
            foreach (Item item in shoppingCartData)
            {
                item.Sold = true;
            }
            // Clear the cart
            shoppingCartData.Clear();

            itemsBinding.DataSource = store.Items.Where(x => x.Sold == false).ToList();

            shoppingCartBinding.ResetBindings(false);
            itemsBinding.ResetBindings(false);
        }
    }
}
