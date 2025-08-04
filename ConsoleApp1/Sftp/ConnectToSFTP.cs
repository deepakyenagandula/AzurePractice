using System;
using Renci.SshNet; // Make sure to add this using directive
using Renci.SshNet.Sftp;
using System.IO;
using Renci.SshNet.Common;

public class SftpConnector
{
    public static void SftpOperations(string[] args)
    {
        // SFTP Server Details (your Ubuntu PC)
        string sftpHost = "192.168.21.240"; // Your Ubuntu PC's IP address
        int sftpPort = 22;                  // SFTP port (default for SSH/SFTP)
        string sftpUser = "sftpuser";       // The SFTP user you created
        string sftpPassword = "your_sftpuser_password"; // The password for sftpuser

        // Path to the 'data' folder on the SFTP server (relative to the chroot)
        string remoteDataFolderPath = "/data"; 

        Console.WriteLine($"Attempting to connect to SFTP server: {sftpHost}:{sftpPort} as {sftpUser}");

        using (var client = new SftpClient(sftpHost, sftpPort, sftpUser, sftpPassword))
        {
            try
            {
                client.Connect();
                Console.WriteLine("Successfully connected to SFTP server!");

                // Check if the remote directory exists and list its contents
                if (client.Exists(remoteDataFolderPath))
                {
                    Console.WriteLine($"Listing contents of {remoteDataFolderPath}:");
                    var filesAndFolders = client.ListDirectory(remoteDataFolderPath);

                    foreach (var item in filesAndFolders)
                    {
                        // Exclude '.' and '..' (current and parent directory references)
                        if (item.Name != "." && item.Name != "..")
                        {
                            Console.WriteLine($"  - {item.Name} (Type: {GetItemType(item)})");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"Remote directory '{remoteDataFolderPath}' does not exist or is not accessible.");
                }

                // --- Example: Upload a file ---
                // Create a dummy file on your local machine for testing
                string localFilePath = "C:\\Temp\\test_upload.txt"; // Adjust this path for your local system
                if (!Directory.Exists("C:\\Temp")) // Ensure the local directory exists
                {
                    Directory.CreateDirectory("C:\\Temp");
                }
                File.WriteAllText(localFilePath, "Hello from C#! This is a test upload.");
                Console.WriteLine($"Created local test file: {localFilePath}");

                using (var fileStream = new FileStream(localFilePath, FileMode.Open))
                {
                    string remoteFileName = Path.GetFileName(localFilePath);
                    string remoteUploadPath = $"{remoteDataFolderPath}/{remoteFileName}";
                    client.UploadFile(fileStream, remoteUploadPath);
                    Console.WriteLine($"Successfully uploaded '{remoteFileName}' to '{remoteUploadPath}' on the SFTP server.");
                }

                // --- Example: Download a file ---
                // Assuming 'test_upload.txt' exists now after upload
                string remoteDownloadFilePath = $"{remoteDataFolderPath}/test_upload.txt";
                string localDownloadPath = "C:\\Temp\\downloaded_file.txt"; // Where to save it locally

                if (client.Exists(remoteDownloadFilePath))
                {
                    using (var fileStream = new FileStream(localDownloadPath, FileMode.Create))
                    {
                        client.DownloadFile(remoteDownloadFilePath, fileStream);
                        Console.WriteLine($"Successfully downloaded '{remoteDownloadFilePath}' to '{localDownloadPath}'.");
                    }
                }
                else
                {
                Console.WriteLine($"Remote file '{remoteDownloadFilePath}' not found for download.");
                }

            }
            catch (SshAuthenticationException authEx)
            {
                Console.WriteLine($"Authentication failed: {authEx.Message}. Check username and password.");
            }
            catch (SshConnectionException connEx)
            {
                Console.WriteLine($"Connection failed: {connEx.Message}. Check IP, Port, and if SFTP server is running.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            finally
            {
                if (client.IsConnected)
                {
                    client.Disconnect();
                    Console.WriteLine("Disconnected from SFTP server.");
                }
            }
        }
    }

    private static string GetItemType(SftpFile item)
    {
        if (item.IsDirectory) return "Directory";
        if (item.IsRegularFile) return "File";
        return "Other";
    }
}
