using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class Server : MonoBehaviour
{
    private TcpListener server;
    private TcpClient client;
    private NetworkStream stream;
    private Thread listenThread;

    public int port = 5000;

    void Start()
    {
        StartServer();
    }

    void StartServer()
    {
        try
        {
            server = new TcpListener(IPAddress.Any, port);
            server.Start();
            Debug.Log("Servidor iniciado en el puerto " + port);

            listenThread = new Thread(ListenForClients);
            listenThread.IsBackground = true;
            listenThread.Start();
        }
        catch (Exception e)
        {
            Debug.LogError("Error al iniciar el servidor: " + e.Message);
        }
    }

    void ListenForClients()
    {
        while (true)
        {
            client = server.AcceptTcpClient();
            Debug.Log("Cliente conectado");
            stream = client.GetStream();

            // Inicia un hilo para manejar la comunicación con el cliente
            Thread clientThread = new Thread(HandleClientCommunication);
            clientThread.Start(client);
        }
    }

    void HandleClientCommunication(object clientObj)
    {
        TcpClient tcpClient = (TcpClient)clientObj;
        NetworkStream clientStream = tcpClient.GetStream();

        byte[] buffer = new byte[1024];
        int bytesRead;

        while (true)
        {
            try
            {
                bytesRead = clientStream.Read(buffer, 0, buffer.Length);
                if (bytesRead == 0)
                {
                    Debug.Log("Cliente desconectado");
                    break;
                }

                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Debug.Log("Mensaje recibido: " + message);

                // Responde al cliente (opcional)
                byte[] response = Encoding.UTF8.GetBytes("Mensaje recibido: " + message);
                clientStream.Write(response, 0, response.Length);
            }
            catch (Exception e)
            {
                Debug.LogError("Error en la comunicación: " + e.Message);
                break;
            }
        }

        tcpClient.Close();
    }

    void OnApplicationQuit()
    {
        if (server != null)
        {
            server.Stop();
        }
    }
}