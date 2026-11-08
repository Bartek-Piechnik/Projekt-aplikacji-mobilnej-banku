using System;
using System.Collections.Generic;
using System.Text;

namespace Aplikacja_bankowa.Helpers
{
    public static class ButtonAnimations
    {
        public static async Task AnimateButtonAsync(Button button)
        {
            await button.ScaleToAsync(1.1, 150);
            await button.ScaleToAsync(1.0, 150);
        }
    }
}
