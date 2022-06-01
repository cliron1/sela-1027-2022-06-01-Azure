using Azure.Core.Extensions;
using Azure.Storage.Blobs;
//using Azure.Storage.Queues;
using Microsoft.Extensions.Azure;

internal static class AzureClientFactoryBuilderExtensions {
	public static IAzureClientBuilder<BlobServiceClient, BlobClientOptions> AddBlobServiceClientUsingMsi(this AzureClientFactoryBuilder builder,
			string serviceUriOrConnectionString) {

		if (Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri? serviceUri)) {
			return builder.AddBlobServiceClient(serviceUri);
		}
		return null;
	}

	//public static IAzureClientBuilder<QueueServiceClient, QueueClientOptions> AddQueueServiceClientUsingMsi(this AzureClientFactoryBuilder builder, string serviceUriOrConnectionString) {
	//	if (Uri.TryCreate(serviceUriOrConnectionString, UriKind.Absolute, out Uri? serviceUri)) {
	//		return builder.AddQueueServiceClient(serviceUri);
	//	}
	//	return null;
	//}
}
