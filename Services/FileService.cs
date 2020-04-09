using System.IO;
using Microsoft.AspNetCore.Http;

namespace CFT.Services
{
	public class FileService
	{
		public byte[] UploadFileInBd(IFormFile file)
		{
			byte[] result;
			using (var ms = new MemoryStream())
			{
				file.CopyTo(ms);
				result = ms.ToArray();
			}

			return result;
		}
	}
}
