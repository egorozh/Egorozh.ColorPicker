using System.Drawing;

namespace Egorozh.ColorPicker
{
    public interface IColorClient
    {
        void ColorUpdated(Color color, IColorClient client);
        
        void Init(IColorManager colorManager);
    }
}