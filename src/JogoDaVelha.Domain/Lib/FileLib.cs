using JogoDaVelha.Domain.Modelo;
using Newtonsoft.Json;
using System;
using System.IO;

namespace JogoDaVelha.Domain.Lib
{
    public static class FileLib
    {
        private static string folderName { get { return "LocalStorage_JogoDaVelha"; } }
        private static string pathLocalStorage { get { return Path.Combine(Directory.GetCurrentDirectory(), folderName); } }

        private static void CheckDirectory()
        {
            if (Directory.Exists(pathLocalStorage) == false)
                Directory.CreateDirectory(folderName);
        }


        public static void CreateJsonFile(GameModel gameModel)
        {
            CheckDirectory();

            var jsonSerializado = JsonConvert.SerializeObject(gameModel);
            File.WriteAllText(Path.Combine(pathLocalStorage, gameModel.Id + ".json"), jsonSerializado);
        }

        public static GameModel GetJsonFile(Guid id)
        {
            try
            {
                CheckDirectory();

                var jsonFile = File.ReadAllText(Path.Combine(pathLocalStorage, id + ".json"));
                GameModel gameModel = JsonConvert.DeserializeObject<GameModel>(jsonFile);
                return gameModel;
            }
            catch (Exception ex)
            {
                throw new Exception("Partida não encontrada.");
            }
        }
    }
}