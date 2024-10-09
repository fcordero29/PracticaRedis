using StackExchange.Redis;
using System;

class RedisPubSub
{
    static void Main(string[] args)
    {
        // Conectar a Redis
        var redis = ConnectionMultiplexer.Connect("redis-17715.c258.us-east-1-4.ec2.redns.redis-cloud.com:17715, password=4NF7rILGRVMl3nqVVOknIfa5gr9ow9Ta");
        var pubsub = redis.GetSubscriber();

        Console.WriteLine("¿Deseas ser Publicador o Suscriptor? (P/S): ");
        var opcion = Console.ReadLine().ToUpper();

        if (opcion == "P")
        {
            // Modo Publicador
            while (true)
            {
                Console.WriteLine("Escribe un mensaje para publicar (o 'salir' para terminar): ");
                var message = Console.ReadLine();
                if (message.ToLower() == "salir")
                    break;

                // Publicar mensaje en el canal 'canal_de_ejemplo'
                pubsub.Publish("canal_de_ejemplo", message);
                Console.WriteLine($"Mensaje publicado: {message}");
            }
        }
        else if (opcion == "S")
        {
            // Modo Suscriptor
            Console.WriteLine("Suscribiéndose al canal 'canal_de_ejemplo'...");
            pubsub.Subscribe("canal_de_ejemplo", (channel, message) => {
                Console.WriteLine($"Mensaje recibido: {message}");
            });

            // Mantener el programa en ejecución
            Console.WriteLine("Esperando mensajes... Presiona Enter para salir.");
            Console.ReadLine();  // Para mantener el programa corriendo
        }
        else
        {
            Console.WriteLine("Opción inválida. Ejecuta el programa de nuevo e ingresa 'P' o 'S'.");
        }
    }
}
