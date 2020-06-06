using System;

using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace HelloWorld

{

class Program

{

// It's always a good idea to access services in an async fashion

static async Task Main()

{

await RecognizeSpeechAsync();

}

static async Task RecognizeSpeechAsync()

{

// Configure the subscription information for the service to access.

// Use either key1 or key2 from the Speech Service resource you have created

var config = SpeechConfig.FromSubscription("KEY", "southcentralus");

// Setup the audio configuration, in this case, using a file that is in local storage.

using (var audioInput = AudioConfig.FromWavFileInput("narration.wav"))

// Pass the required parameters to the Speech Service which includes the configuration information

// and the audio file name that you will use as input

using (var recognizer = new SpeechRecognizer(config, audioInput))

{

Console.WriteLine("Reconociendo primeros resultados :)!!...");

var result = await recognizer.RecognizeOnceAsync();

switch (result.Reason)

{

case ResultReason.RecognizedSpeech:

// The file contained speech that was recognized and the transcription will be output

// to the terminal window

Console.WriteLine($"He reconocido🎤👀: {result.Text}");

break;

case ResultReason.NoMatch:

// No recognizable speech found in the audio file that was supplied.

// Out an informative message

Console.WriteLine($"NOMATCH: No he podido reconocer nada :().");

break;

case ResultReason.Canceled:

// Operation was cancelled

// Output the reason

var cancellation = CancellationDetails.FromResult(result);

Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

if (cancellation.Reason == CancellationReason.Error)

{

Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");

Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");

Console.WriteLine($"CANCELED: Has actualizdo tu informacion de suscripcion?");

}

break;

}

}

}

}

}









/* using System;

using System.Threading.Tasks;

using Microsoft.CognitiveServices.Speech;

namespace helloworld

{

class Program

{

public static async Task RecognizeSpeechAsync()

{

var config = SpeechConfig.FromSubscription("KEY", "southcentralus");

using (var recognizer = new SpeechRecognizer(config))

{

var result = await recognizer.RecognizeOnceAsync();

if (result.Reason == ResultReason.RecognizedSpeech)

{

Console.WriteLine($"We recognized: {result.Text}");

}

else if (result.Reason == ResultReason.NoMatch)

{

Console.WriteLine($"NOMATCH: Speech could not be recognized.");

}

else if (result.Reason == ResultReason.Canceled)

{

var cancellation = CancellationDetails.FromResult(result);

Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

if (cancellation.Reason == CancellationReason.Error)

{

Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");

Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");

Console.WriteLine($"CANCELED: Did you update the subscription info?");

}

}

}

}

static void Main()

{

Console.WriteLine("Begin speaking....");

RecognizeSpeechAsync().Wait();

Console.WriteLine("Please press <Return> to continue.");

Console.ReadLine();

}

}

} */