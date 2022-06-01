using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using SignalRChat.Models;
using System.Diagnostics;

namespace SignalRChat.Controllers {
	public class HomeController : Controller {
		private readonly BlobServiceClient storage;
		private readonly ILogger<HomeController> _logger;

		public HomeController(BlobServiceClient storage, ILogger<HomeController> logger) {
			this.storage = storage;
			_logger = logger;
		}

		public IActionResult Index() {
			var containerClient = storage.GetBlobContainerClient("public");
			//var blob = containerClient.GetBlobClient("kitten.jpg");
			//var model = blob.Uri.ToString();

			var list = containerClient.GetBlobs();

			var ret = new List<PicModel>();
			foreach (var webpItem in list.Where(x => Path.GetExtension(x.Name) == ".webp")) {
				var blob = containerClient.GetBlobClient(webpItem.Name);
				var fallBackBlob = list.FirstOrDefault(x =>
					Path.GetFileNameWithoutExtension(x.Name) == Path.GetFileNameWithoutExtension(webpItem.Name) &&
					Path.GetExtension(x.Name) != ".webp"
				);
				var item = new PicModel {
					WebpSrc = blob.Uri.ToString(),
					Name = webpItem.Name,
					FallbackSrc = fallBackBlob!=null
						? containerClient.GetBlobClient(fallBackBlob.Name).Uri.ToString()
						: null
				};
				ret.Add(item);
			}

			return View(ret);
		}

		public IActionResult Privacy() {
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}