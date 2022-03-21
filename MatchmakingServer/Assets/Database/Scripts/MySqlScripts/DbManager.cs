using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;



public class DbManager : MonoBehaviour
{
    #region Singleton
    public static DbManager Instance { set; get; }

    private void Awake()
    {
        Instance = this;
    }
    #endregion
    public DatabaseConnection databaseConnection;
    private MySqlCommand cmd;
    private MySqlDataReader dataReader;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //CheckLoginAccount("jovanjoca.stosic998@gmail.com", "123456");
            //CreateAccount("Jov123gmail","JOLE", "123456");
            //AddFirend("ZQ88HNUAA4GL", "3SRHIERYC6Q3");
            // GetAllFriends("ZQ88HNUAA4GL");
            // Debug.Log(EmptyPort());
            // CreateGame("192.168.0.15","27001", entity1, entity2);
            EmptyPort();

        }
    }
    public void CreateGame(string ipAddress, string port, Entity entity1, Entity entity2)
    {
        if (databaseConnection.OpenConnection(databaseConnection))
        {
            cmd = databaseConnection.dbSQLConnection.CreateCommand();
            cmd.CommandText = "INSERT INTO game(ip,port,entity1ID,entity2ID,E1Class,E2Class) " +
                        "Values(@ip,@port,@entity1ID,@entity2ID,@E1Class,@E2Class)";
            cmd.Parameters.AddWithValue("@ip", ipAddress);
            cmd.Parameters.AddWithValue("@port", port);
            cmd.Parameters.AddWithValue("@entity1ID", entity1.SpecialID);
            cmd.Parameters.AddWithValue("@entity2ID", entity2.SpecialID);
            cmd.Parameters.AddWithValue("@E1Class", entity1.Class);
            cmd.Parameters.AddWithValue("@E2Class", entity2.Class);

            try
            {
                if (cmd.ExecuteNonQuery() == 0)
                {
                    print("The Query Not Completed");
                }

                else
                {
                    print("Mission Completed");

                }
            }
            catch (Exception e)
            {
                print(e.Message);
            }
        }
        else
        {
            print("Error at connection");

        }
    }
    public string EmptyPort()
    {
        string free_port = "";
        if (databaseConnection.OpenConnection(databaseConnection))
        {
            string query = "SELECT port FROM servergame " +
                    " WHERE isBusy = '0'";
            dataReader = excuteQuery(query);
            try
            {
                if (dataReader.HasRows)
                {
                    dataReader.Read();
                    free_port = dataReader.GetString(0);
                    dataReader.Close();

                    MySqlCommand comm = databaseConnection.dbSQLConnection.CreateCommand();
                    comm.CommandText = "UPDATE servergame SET isBusy ='1'" +
                        "WHERE port = '" + free_port + "'";
                    try
                    {
                        if (comm.ExecuteNonQuery() == 0)
                        {
                            print("Sorry Query not completed");
                        }

                        else
                        {
                            print("Data Updated successfuly");

                        }
                    }
                    catch (Exception e)
                    {
                        print(e.Message);
                    }


                    databaseConnection.CloseConnection();
                }
                else
                {
                    databaseConnection.CloseConnection();
                    Debug.Log("There is no free port");
                }
            }
            catch (Exception e)
            {
                print(e.Message);
            }
        }

        return free_port;
    }
    public List<User> GetAllFriends(string specialID)
    {
        if(specialID != "")
        {
            if (databaseConnection.OpenConnection(databaseConnection))
            {
                string query = "SELECT friendSID FROM friends " +
                      " WHERE mineSID = '" + specialID + "'";
                //SELECT `friendSID` FROM `friends` WHERE `mineSID` = 'ZQ88HNUAA4GL'

                dataReader = excuteQuery(query);

                List<string> ids = new List<string>();
                try
                {
                    while (dataReader.Read())
                    {
                        ids.Add(dataReader.GetString(0));
                    }
                    if (ids.Count != 0)
                    {
                        dataReader.Close();

                        List<User> friends = new List<User>();
                        foreach (string id in ids)
                        {
                            string q = "SELECT nickname FROM account " +
                                " WHERE specialID = '" + id + "'";
                            dataReader = excuteQuery(q);
                            try
                            {
                                if (dataReader.HasRows)
                                {
                                    dataReader.Read();
                                    User user = new User();
                                    user.Nickname = dataReader.GetString(0);
                                    user.SpecialID = id;
                                    user.isOnline = Server.Instance.isUserOnline(id);
                                    if (user.isOnline == 1)
                                        user.Connection = Server.Instance.UserConnection(id);
                                    Debug.Log("Friend: " + user.Nickname + " specialID: " + user.SpecialID + " isOnline: " + user.isOnline);
                                    friends.Add(user);
                                    dataReader.Close();
                                }
                            }
                            catch (Exception e)
                            {
                                print(e.Message);
                                return null;
                            }

                        }
                        dataReader.Close();
                        databaseConnection.CloseConnection();
                        return friends;
                        
                    }
                    else
                    {
                        dataReader.Close();
                        databaseConnection.CloseConnection();
                        return null;
                    }
                }
                catch (Exception e)
                {
                    print(e.Message);
                    return null;
                }
            }
            else
            {
                databaseConnection.CloseConnection();
                return null;
            }
        }
        return null;
    }
    public void AddFirend(string mineID, string friendID)
    {
        if (mineID != "" && friendID != "")
        {
            if (databaseConnection.OpenConnection(databaseConnection))
            {
                cmd = databaseConnection.dbSQLConnection.CreateCommand();
                cmd.CommandText = "INSERT INTO friends(mineSID,friendSID) " +
                            "Values(@mineSID,@friendSID)";
                cmd.Parameters.AddWithValue("@mineSID", mineID);
                cmd.Parameters.AddWithValue("@friendSID", friendID);

                try
                {
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        print("The Query Not Completed");
                    }

                    else
                    {
                        cmd = databaseConnection.dbSQLConnection.CreateCommand();
                        cmd.CommandText = "INSERT INTO friends(mineSID,friendSID) " +
                                    "Values(@mineSID,@friendSID)";
                        cmd.Parameters.AddWithValue("@mineSID", friendID);
                        cmd.Parameters.AddWithValue("@friendSID", mineID);

                        if (cmd.ExecuteNonQuery() == 0)
                        {
                            print("The Query Not Completed");
                        }
                        else
                        {
                            print("Mission compplete");
                        }

                    }
                }
                catch (Exception e)
                {
                    print(e.Message);
                }
            }
        }
    }
    public string CreateAccount(string email, string nickname, string password)
    {
        string id = "";
        if(email != "" && nickname != "" && password != "")
        {
            if (databaseConnection.OpenConnection(databaseConnection))            
            {
                cmd = databaseConnection.dbSQLConnection.CreateCommand();
                cmd.CommandText = "INSERT INTO account(email,password,nickname,rank,specialID) " +
                           "Values(@email,@password,@nickname,@rank,@specialID)";
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@nickname", nickname);
                cmd.Parameters.AddWithValue("@rank", 500);
                id = getRandomID();
                cmd.Parameters.AddWithValue("@specialID", id);
                //cmd.Parameters.Add("@image", MySqlDbType.Blob).Value = bytes;

                try
                {
                    if (cmd.ExecuteNonQuery() == 0)
                    {
                        print("The Query Not Completed");
                    }

                    else
                    {
                        print("Mission Completed");

                    }
                }
                catch (Exception e)
                {
                    print(e.Message);
                }
            }
            else
            {
                print("Error at connection");

            }
        }

        return id;
    }

    public List<string> CheckLoginAccount(string email, string password)
    {
        List<string> data = new List<string>();
        if (email != "" && password != "")
        {
            if (databaseConnection.OpenConnection(databaseConnection))
            {
                string query = "SELECT Nickname, Rank, specialID FROM account " +
                      " WHERE Email = '" + email + "' AND Password = '" + password + "'";

                dataReader = excuteQuery(query);
                try
                {
                    if (dataReader.HasRows)
                    {
                        dataReader.Read();
                        data.Add(dataReader.GetString(0));
                        data.Add(dataReader.GetString(1));
                        data.Add(dataReader.GetString(2));
                        print("Exist in database");
                        dataReader.Close();
                        databaseConnection.CloseConnection();
                        return data;
                    }
                    else
                    {
                        databaseConnection.CloseConnection();
                        Debug.Log("User doesnot exist in database");
                        return null;
                    }
                        
                    /*while (dataReader.Read())
                    {
                        Debug.Log(dataReader.GetString(0));
                        Debug.Log("Exist");
                    }*/
                }
                catch (Exception e)
                {
                    print(e.Message);
                    return null;
                }
            }
            else
            {
                databaseConnection.CloseConnection();
                return null;
            }
        }
        else
            return null;
    }


    MySqlDataReader excuteQuery(string query)
    {
        cmd = new MySqlCommand(query, databaseConnection.dbSQLConnection);
        cmd.CommandTimeout = 60;
        return cmd.ExecuteReader();
    }

    public string getRandomID()
    {
        string id = string.Empty;
        for (int i = 0; i < 12; i++)
        {
            int random = UnityEngine.Random.Range(0, 36);
            if (random < 26)
                id += (char)(random + 65);
            else
                id += (random - 26).ToString();
        }

        return id;
    }

    public static Texture2D LoadImage(string filename) {
        byte[] bytes = File.ReadAllBytes(filename);

        Texture2D texture = new Texture2D(64, 64, TextureFormat.RGB24, false);
        texture.LoadImage(bytes);

        /////////
        ///Ucitavanje slike sa putanje na kompu/
        ////Texture2D tex = LoadImage("C:/Users/Jovan/Desktop/hexagon.png");
        //sprite.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        //byte[] bytes = ImageConversion.EncodeArrayToJPG(tex.GetRawTextureData(), tex.graphicsFormat, (uint)tex.width, (uint)tex.height);


        //byte[] bytes = tex.EncodeToJPG();
        //Debug.Log(Application.dataPath);
        // File.WriteAllBytes(Application.dataPath + "/hexagon.jpg", bytes);
        // sprite.sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        /*Texture2D texture = new Texture2D(64, 64, TextureFormat.RGB24, false);
        texture.LoadImage(bytes);

        sprite.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);*/

        return texture;
    }
}




/* public void InsertData()
 {
     if (userName.text != "" && userMail.text != "")
     {
         if (databaseConnection.OpenConnection(databaseConnection))
         {
             MySqlCommand comm = databaseConnection.dbSQLConnection.CreateCommand();
             comm.CommandText = "INSERT INTO login(name,mail) " +
                        "Values(@name,@mail)";
             comm.Parameters.AddWithValue("@name", userName.text);
             comm.Parameters.AddWithValue("@mail", userMail.text);

             try
             {
                 if (comm.ExecuteNonQuery() == 0)
                 {
                     print("The Query Not Completed");
                 }

                 else
                 {
                     print("Mission Completed");

                 }
             }
             catch (Exception e)
             {
                 print(e.Message);
             }
         }
         else
         {
             print("Error at connection");

         }
     }

 }

 public void UpdateData()
 {
     if (userName.text != "" && userMail.text != "")
     {
         if (databaseConnection.OpenConnection(databaseConnection))
         {
             MySqlCommand comm = databaseConnection.dbSQLConnection.CreateCommand();
             comm.CommandText = "Update testdetails Set name ='" + userName.text + "'" +
                 ",mail='" + userMail.text + "' Where name ='" + userName.text + "'";
             try
             {
                 if (comm.ExecuteNonQuery() == 0)
                 {
                     print("Sorry Query not completed");
                 }

                 else
                 {
                     print("Data Updated successfuly");

                 }
             }
             catch (Exception e)
             {
                 print(e.Message);
             }
         }
         else
         {
             print("Sorry there's problem with connection");
         }
     }



 }

 public void SelectUser()
 {
     if (userName.text != "")
     {
         if (databaseConnection.OpenConnection(databaseConnection))
         {

             string query = "SELECT mail,name from testdetails" +
                 " WHERE name = '" + userName.text + "'";
             rdr = excuteQuery(query);

             try
             {

                 if (rdr.HasRows)
                 {
                     rdr.Read();
                     print("logined");
                     rdr.Close();
                     databaseConnection.CloseConnection();
                 }
             }
             catch (Exception e)
             {
                 print(e.Message);
             }
         }
         else
         {
             databaseConnection.CloseConnection();
             return;
         }
     }
 }

 public void DeleteUser()
 {
     if (userName.text != "")
     {
         if (databaseConnection.OpenConnection(databaseConnection))
         {
             MySqlCommand comm = databaseConnection.dbSQLConnection.CreateCommand();
             comm.CommandText = "Delete From testdetails Where name= '" + userName.text + "' ";
             try
             {
                 if (comm.ExecuteNonQuery() == 0)
                 {
                     print("Sorry Query not completed");

                 }
                 else
                 {
                     print("Data Deleted successfuly");

                 }
             }
             catch (Exception e)
             {
                 print(e.Message);

             }
         }
         else
         {
             print("Sorry there's problem with connection");

         }
     }

 }*/
