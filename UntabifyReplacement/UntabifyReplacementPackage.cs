using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using CMcG.UntabifyReplacement;


namespace CMcG.UntabifyReplacement
{
    [InstalledProductRegistration("#110", "#112", "4.0")]
    [Guid(GuidList.guidUntabifyReplacementPkgString)]
    public class UntabifyReplacementPackage : Package
    {
    }
}