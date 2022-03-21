using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Sockets;
using MySql.Data.MySqlClient;

[CreateAssetMenu(fileName = "DataBase", menuName = "DataBase/Create Connection", order = 51)]
public class DatabaseConnection : ScriptableObject
{

    [HideInInspector]
    public MySqlConnection dbSQLConnection;

    [Header("DataBase Connection")]
    public string dataBaseDataSource;
    public string databaseName;
    [Header("DataBase privilages")]
    public string databaseUserName;
    public string databasePassword;
    public UInt16 databasePort;

    public bool OpenConnection(DatabaseConnection databaseConnection)
    {
        MySqlConnectionStringBuilder connString = new MySqlConnectionStringBuilder();
        connString.Server = databaseConnection.dataBaseDataSource;
        connString.UserID = databaseConnection.databaseUserName;
        connString.Password = databaseConnection.databasePassword;
        connString.Port = databaseConnection.databasePort;
        connString.Database = databaseConnection.databaseName;
        connString.CharacterSet = "utf8";
        dbSQLConnection = new MySqlConnection(connString.ToString());
        try
        {
            dbSQLConnection.Open();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public void CloseConnection()
    {
        dbSQLConnection.Close();
    }

}
