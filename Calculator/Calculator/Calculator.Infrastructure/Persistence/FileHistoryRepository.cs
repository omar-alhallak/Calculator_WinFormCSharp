using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;
using Calculator.Calculator.Core.Model;

namespace Calculator.Calculator.Infrastructure.Persistence
{
    public sealed class FileHistoryRepository // مسؤل عن القراءة من الملف
    {
        private readonly string FilePath;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        public FileHistoryRepository(string filePath)
        {
            FilePath = filePath;
            EnsureDirectory();
        }

        private void EnsureDirectory() // تنظيم المجلد و تجهيزه
        {
            string? dir = Path.GetDirectoryName(FilePath);
            if (!string.IsNullOrWhiteSpace(dir))
                Directory.CreateDirectory(dir);
        }

        public IReadOnlyList<HistoryEntry> Load() // قراءة الملف وتحويله ل ليست
        {
            try
            {
                EnsureDirectory();

                if (!File.Exists(FilePath))
                    return new List<HistoryEntry>();

                string json = File.ReadAllText(FilePath);
                if (string.IsNullOrWhiteSpace(json))
                    return new List<HistoryEntry>();

                return JsonSerializer.Deserialize<List<HistoryEntry>>(json, JsonOptions) ?? new List<HistoryEntry>();
            }
            catch(JsonException)
            {
                return new List<HistoryEntry>();
            }
            catch (IOException)
            {
                return new List<HistoryEntry>();
            }
        }

        public void Save(IEnumerable<HistoryEntry> items) // تأكد من وجود المجلد
        {
            EnsureDirectory();

            string json = JsonSerializer.Serialize(items, JsonOptions);
            string tmp = FilePath + ".tmp";
            File.WriteAllText(tmp, json);
            File.Move(tmp, FilePath, true);
        }
    }
}