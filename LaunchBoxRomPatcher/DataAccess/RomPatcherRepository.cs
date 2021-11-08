using LaunchBoxRomPatcher.Helpers;
using LaunchBoxRomPatcher.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LaunchBoxRomPatcher.DataAccess
{
    public sealed class RomPatcherRepository
    {
        private static readonly RomPatcherRepository instance = new RomPatcherRepository();

        // explicit static constructor to tell C# compiler not to mark type as beforefieldinit 
        static RomPatcherRepository()
        {
        }

        private RomPatcherRepository()
        {
            // create an empty data file if it doesn't exist
            DirectoryInfoHelper.CreateFileIfNotExists(_dataFilePath);
        }

        public static RomPatcherRepository Instance
        {
            get
            {
                return instance;
            }
        }

        private string _dataFilePath = DirectoryInfoHelper.Instance.RomPatcherDataFilePath;

        private List<RomPatcher> _romPatchers;

        public string ErrorMessage { get; private set; }

        // read the data file into memory 
        public async Task InitializeData()
        {
            try
            {
                // reset error message 
                ErrorMessage = string.Empty;

                // read the RomPatcher data file 
                using (StreamReader r = new StreamReader(_dataFilePath))
                {
                    string json = r.ReadToEnd();
                    _romPatchers = await Task.Run(() => JsonConvert.DeserializeObject<List<RomPatcher>>(json));
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                LogHelper.LogException(ex, $"InitializeData for {_dataFilePath ?? ""}");
            }
            finally
            {
                // create an empty list if nothing was in the file
                if (_romPatchers == null)
                {
                    _romPatchers = new List<RomPatcher>();
                }
            }
        }

        public async Task<List<RomPatcher>> GetAllAsync()
        {
            await InitializeData();

            return _romPatchers;
        }

        public async Task<RomPatcher> GetByIdAsync(string id)
        {
            if (_romPatchers == null)
            {
                await InitializeData();
            }

            RomPatcher romPatcher = _romPatchers.First(romPatcher => romPatcher.RomPatcherId == id);

            return romPatcher;
        }

        public async Task SaveRomPatcher(RomPatcher romPatcher)
        {
            // read the data file 
            await GetAllAsync();

            // find the rom patcher with specified id
            RomPatcher existingRomPatcher = await GetByIdAsync(romPatcher?.RomPatcherId);

            // update it
            if(existingRomPatcher == null)
            {
                existingRomPatcher = new RomPatcher();
                existingRomPatcher.RomPatcherId = new Guid().ToString();
            }
            existingRomPatcher.RomPatcherName = romPatcher.RomPatcherName;
            existingRomPatcher.RomPatcherFilePath = romPatcher.RomPatcherFilePath;
            existingRomPatcher.RomPatcherCommandLine = romPatcher.RomPatcherCommandLine;

            // write the file 
            await WriteAllAsync();
        }

        private async Task WriteAllAsync()
        {
            try
            {
                // reset error message 
                ErrorMessage = string.Empty;

                BackupDataFile();

                // write the file 
                await Task.Run(() =>
                {
                    string json = JsonConvert.SerializeObject(_romPatchers);
                    File.WriteAllText(_dataFilePath, json);
                });
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                LogHelper.LogException(ex, $"InitializeData for {_dataFilePath ?? ""}");
            }
        }

        public async Task<bool> RomPatcherHasChanges(RomPatcher romPatcher)
        {
            await GetAllAsync();
            RomPatcher originalRomPatcher = await GetByIdAsync(romPatcher.RomPatcherId);

            if (originalRomPatcher.RomPatcherName != romPatcher.RomPatcherName) return true;
            if (originalRomPatcher.RomPatcherFilePath != romPatcher.RomPatcherFilePath) return true;
            if (originalRomPatcher.RomPatcherCommandLine != romPatcher.RomPatcherCommandLine) return true;

            return false;
        }

        private void BackupDataFile()
        {
            // copy the file to backup folder before writing
            if (File.Exists(_dataFilePath))
            {
                string currentTimeString = DateTime.Now.ToString("yyyyMMdd_H_mm_ss");
                string newFilePath = $"{DirectoryInfoHelper.Instance.RomPatcherDataFileBackupPath}\\RomPatchers_{currentTimeString}.json";
                File.Copy(_dataFilePath, newFilePath);
            }
        }
    }
}