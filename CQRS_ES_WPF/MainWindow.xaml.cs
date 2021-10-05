using CQRS_ES.CQRS;
using CQRS_ES.Domain;
using CQRS_ES.ES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CQRS_ES_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EventsStore _eventsStore;
        private Dictionary<Guid, int> _dAggregateId;
        private EventsRepository _eventsRepository;
        private CommandHandler _commandHandler;

        public MainWindow()
        {
            InitializeComponent();
            LayoutComponents();
        }

        public void LayoutComponents()
        {
            _eventsStore = new EventsStore();
            _dAggregateId = new Dictionary<Guid, int>();
            _eventsRepository = new EventsRepository();
            _commandHandler = new CommandHandler(_eventsStore);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFields())
            {
                string _productName = productName.Text;
                Guid _aggregateId;
                try
                {
                    _aggregateId = Guid.Parse(aggregateId.Text);
                }
                catch
                {
                    _aggregateId = Guid.NewGuid();
                    aggregateId.Text = _aggregateId.ToString();
                }
                int _quantity = Convert.ToInt32(quantity.Text);
                decimal _price = Convert.ToDecimal(price.Text);

                Product _product = new Product(_productName, _aggregateId, _quantity, _price);
                int result = _commandHandler.HandleCommand(new AddCommand(_eventsStore, _eventsRepository, _product, _quantity), _dAggregateId);

                if (result != -1)
                    RefreshData();
            }
        }

        private bool CheckFields()
        {
            return !productName.Text.Equals("") &&
                   !aggregateId.Text.Equals("") &&
                   !quantity.Text.Equals("") &&
                   !price.Text.Equals("");
        }

        private void RefreshData()
        {

            EventsPanel.Children.RemoveRange(0, EventsPanel.Children.Count);

            var uncommittedEvents = _eventsRepository.GetUncommittedEvents();
            if (uncommittedEvents.Count > 0)
            {
                foreach (var id in uncommittedEvents)
                {
                    Guid aggregateId = id;
                    int lastSavedEventNumber = _eventsStore.GetLastEventNumber(aggregateId);

                    _eventsStore.SaveEvents(aggregateId, _eventsRepository.Events, lastSavedEventNumber);
                }
            }

            List<string> dataSource = new List<string>();
            foreach (var id in _dAggregateId.Keys)
            {
                QueryModel model = new QueryModel(_eventsStore, id);
                dataSource.AddRange(model.ShowProduct());
            }
            
            foreach (string data in dataSource)
            {
                TextBlock tb = new TextBlock();
                tb.Text = data;
                EventsPanel.Children.Add(tb);
            }
        }
    }
}
