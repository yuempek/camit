using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace NETWORKLIST
{
	[System.Runtime.CompilerServices.CompilerGenerated, System.Runtime.InteropServices.Guid("DCB00001-570F-4A9B-8D69-199FDBA5723B"), System.Runtime.InteropServices.InterfaceType(System.Runtime.InteropServices.ComInterfaceType.InterfaceIsIUnknown), TypeIdentifier]
	[System.Runtime.InteropServices.ComImport]
	public interface INetworkListManagerEvents
	{
		void ConnectivityChanged([System.Runtime.InteropServices.In] NLM_CONNECTIVITY newConnectivity);
	}
}
