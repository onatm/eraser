using Eraser.Model.Network;
using Eraser.ViewModel;

namespace Eraser.Helper
{
    public enum WindowType{
        MODAL
    }

    public class OpenWindowMessage
    {
        public WindowType Type { get; set; }
        public DNS SelectedDNSProvider { get; set; }
        public Adapter SelectedAdapter { get; set; }
    }
}
