using HookUILib.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorAdjustments.UI
{
        public class ColorAdjustmentsUI : UIExtension
        {
            public new readonly string extensionID = "nyoko.coloradjustments";
            public new readonly string extensionContent;
            public new readonly ExtensionType extensionType = ExtensionType.Panel;

        /// <summary>
        /// npx esbuild ui.jsx --bundle --outfile=ui.transpiled.js
        /// </summary>
        public ColorAdjustmentsUI()
            {
                this.extensionContent = this.LoadEmbeddedResource("ColorAdjustments.ui.transpiled.js");
            }
        }
    }


