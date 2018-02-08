using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
namespace NETWORKLIST
{
	[System.Runtime.CompilerServices.CompilerGenerated, TypeIdentifier("dcb00d01-570f-4a9b-8d69-199fdba5723b", "NETWORKLIST.NLM_CONNECTIVITY")]
	public enum NLM_CONNECTIVITY
	{
		NLM_CONNECTIVITY_DISCONNECTED,
		NLM_CONNECTIVITY_IPV4_NOTRAFFIC,
		NLM_CONNECTIVITY_IPV6_NOTRAFFIC,
		NLM_CONNECTIVITY_IPV4_SUBNET = 16,
		NLM_CONNECTIVITY_IPV4_LOCALNETWORK = 32,
		NLM_CONNECTIVITY_IPV4_INTERNET = 64,
		NLM_CONNECTIVITY_IPV6_SUBNET = 256,
		NLM_CONNECTIVITY_IPV6_LOCALNETWORK = 512,
		NLM_CONNECTIVITY_IPV6_INTERNET = 1024
	}
}
