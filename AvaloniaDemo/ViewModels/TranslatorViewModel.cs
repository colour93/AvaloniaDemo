using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaloniaDemo.ViewModels
{
    public class TranslatorItem
    {
        public string RawText { get; set; }
    }


    public class TranslatorViewModel
    {
        public ObservableCollection<TranslatorItem> TranslatorItems { get; }

        public TranslatorViewModel()
        {
            TranslatorItems = new ObservableCollection<TranslatorItem>
            {
                new TranslatorItem { RawText = "Item 1" },
                new TranslatorItem { RawText = "Item 2" },
                new TranslatorItem { RawText = "Item 3" }
            };
        }
    }
}
