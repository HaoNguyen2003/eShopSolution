namespace eShopSolution.WebAPI.Helpers
{
    public class WorkWithFile
    {
        //Image
        public static string AddImage(IFormFile file, string folder)
        {
            try
            {
                var validImageTypes = new List<string>
                {
                     "image/jpeg",
                     "image/png",
                     "image/gif",
                     "image/bmp",
                     "image/tiff",
                      "image/webp"
                };

                if (file != null && validImageTypes.Contains(file.ContentType.ToLower()))
                {
                    string random = Guid.NewGuid().ToString();
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "Image", folder, random + Path.GetExtension(file.FileName));
                    Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Image", folder));
                    using (var myFile = new FileStream(fullPath, FileMode.CreateNew))
                    {
                        file.CopyTo(myFile);
                    }
                    return Path.Combine("Image", folder, random + Path.GetExtension(file.FileName));
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public static async Task<string> UpdateImageAsync(IFormFile file, string folder, string currentImagePath)
        {
            try
            {
                var validImageTypes = new List<string>
        {
            "image/jpeg",
            "image/png",
            "image/gif",
            "image/bmp",
            "image/tiff",
            "image/webp"
        };

                if (file != null && validImageTypes.Contains(file.ContentType.ToLower()))
                {
                    var storagePath = Path.Combine(Directory.GetCurrentDirectory(), "Image", folder);

                    if (!Directory.Exists(storagePath))
                    {
                        Directory.CreateDirectory(storagePath);
                    }

                    if (!string.IsNullOrEmpty(currentImagePath))
                    {
                        var currentFilePath = Path.Combine(Directory.GetCurrentDirectory(), currentImagePath);

                        if (File.Exists(currentFilePath))
                        {
                            File.Delete(currentFilePath);
                        }
                    }

                    string randomFileName = Guid.NewGuid().ToString();
                    var newFilePath = Path.Combine(storagePath, randomFileName + Path.GetExtension(file.FileName));

                    using (var stream = new FileStream(newFilePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return Path.Combine("Image", folder, randomFileName + Path.GetExtension(file.FileName));
                }
                else
                {
                    return currentImagePath;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật ảnh: {ex.Message}");
                return currentImagePath;
            }
        }

        public static async Task<bool> OverrideImageAsync(IFormFile file, string currentImagePath)
        {
            if (file == null)
            {
                return false;
            }

            if (string.IsNullOrEmpty(currentImagePath))
            {
                return false;
            }

            try
            {
                var validImageTypes = new List<string>
                {
                     "image/jpeg",
                     "image/png",
                     "image/gif",
                     "image/bmp",
                     "image/tiff",
                      "image/webp"
                };
                if (file != null && validImageTypes.Contains(file.ContentType.ToLower()))
                {
                    var fullPathCurrent = Path.Combine(Directory.GetCurrentDirectory(), currentImagePath.Replace("/", "\\"));
                    using (var stream = new FileStream(fullPathCurrent, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error overriding image: {ex.Message}");
                return false;
            }
        }

        public static bool IsImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return false;
            }
            try
            {
                var mimeType = file.ContentType.ToLower();
                return mimeType == "image/jpeg" ||
                       mimeType == "image/png" ||
                       mimeType == "image/gif" ||
                       mimeType == "image/bmp" ||
                       mimeType == "image/tiff";
            }
            catch
            {
                return false;
            }
        }

        public static List<string> AddArrayImage(List<IFormFile> files, string folder)
        {
            List<string> list = new List<string>();
            foreach (var file in files)
            {
                list.Add(AddImage(file, folder));
            }
            return list;
        }

        public static bool RemoveFile(string filePath)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                if (File.Exists(fullPath))
                {
                    File.Delete(fullPath);
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static void RemoveArrayFile(List<string> filePath)
        {
            foreach (var file in filePath)
            {
                RemoveFile(file);
            }
        }

    }
}
