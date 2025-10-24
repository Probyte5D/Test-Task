// Libreria per usare i tipi BSON di MongoDB (ObjectId, ecc.)
using MongoDB.Bson;
// Libreria per gli attributi di serializzazione BSON
using MongoDB.Bson.Serialization.Attributes;
// Libreria per usare le liste
using System.Collections.Generic;

namespace TestSystem.Models
{
    // Modello che rappresenta un Test
    public class Test
    {
        // Identificatore unico del Test in MongoDB
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        // Titolo del test
        public string Title { get; set; } = string.Empty;

        // Lista di domande associate al test
        public List<Question> Questions { get; set; } = new List<Question>();
    }

    // Modello che rappresenta una domanda
    public class Question
    {
        // Identificatore unico della domanda
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        // Testo della domanda
        public string Text { get; set; } = string.Empty;

        // Lista di opzioni associate alla domanda
        public List<Option> Options { get; set; } = new List<Option>();
    }

    // Modello che rappresenta un'opzione di risposta
    public class Option
    {
        // Identificatore unico dell'opzione
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        // Testo dell'opzione
        public string Text { get; set; } = string.Empty;

        // Indica se l'opzione è corretta o meno
        public bool IsCorrect { get; set; } = false;
    }

    // Modello che rappresenta il risultato di un utente su un test
    public class UserResult
    {
        // Identificatore unico del risultato
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        // Nome dell'utente
        public string UserName { get; set; } = string.Empty;

        // Id del test a cui si riferisce il risultato
        public string TestId { get; set; } = string.Empty;

        // Numero di risposte corrette dell'utente
        public int CorrectAnswers { get; set; }

        // Numero totale di domande del test
        public int TotalQuestions { get; set; }
    }
}
