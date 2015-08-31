﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace IoTChallenge.Universal.Core
{
    public static partial class documentDB
    {
        /// <summary>
        /// Async method that Deletes a Document given it's Unique ID assigned by Azure 
        /// </summary>
        /// <param name="accountID">Document DB Account name</param>
        /// <param name="dbID">Unique ID generated for the Database by Azure</param>
        /// <param name="collectionID">Unique ID generated for the Collection by Azure</param>
        /// <param name="jsonFile">Json formated string that contains the body of the Document 
        /// that will be created in DocumentDB</param>
        /// <returns>A message that indicates the success or failure of the request. 
        /// More info on status code at https://msdn.microsoft.com/en-us/library/azure/dn803948.aspx </returns>
        public static async Task<string> createDocument(string accountID, string dbID, string collectionID, string jsonFile)
        {
            string url = "https://{0}.documents.azure.com/dbs/{1}/colls/{2}/docs";
            url = String.Format(url, accountID, dbID, collectionID);

            StringContent body = GetStringContent(jsonFile);
            HttpClient client = await GetClient("post", accountID, collectionID);

            try
            {
                HttpResponseMessage result = await client.PostAsync(url, body);
                return result.StatusCode.ToString() + " Documento creado";
            }
            catch (Exception ex) { return ex.Message; }
        }

        /// <summary>
        /// Async method that returns a List of genericDocuments that contain the 
        /// assigned ID by the User and the Unique ID generated by Azure, given the unique ID of the 
        /// Collection that contains the Documents
        /// </summary>
        /// <param name="accountID">Document DB Account name</param>
        /// <param name="dbID">Unique ID generated for the Database by Azure</param>
        /// <param name="collectionID">Unique ID generated for the Collection by Azure</param>
        /// <returns>A List of GenericDocument objects OR a null value if the request fails. 
        /// (Still working a more decent message return when the operation fails)
        /// More info on status code at https://msdn.microsoft.com/en-us/library/azure/dn803955.aspx </returns>
        public static async Task<List<genericDocument>> getDocuments(string accountID, string dbID, string collectionID)
        {
            string url = "https://{0}.documents.azure.com/dbs/{1}/colls/{2}/docs";
            url = String.Format(url, accountID, dbID, collectionID);
            HttpClient client = await GetClient("get", accountID, collectionID);
            
            try
            {
                string result = await client.GetStringAsync(url);
                genericDocumentsCollection documents = JsonConvert.DeserializeObject<genericDocumentsCollection>(result);
                return documents.Documents;
            }
            catch (Exception ex) { return null; }
        }

        /// <summary>
        /// Async method that returns a Document given it's Unique ID assigned 
        /// by Azure when the Document was created
        /// </summary>
        /// <param name="accountID">Document DB Account name</param>
        /// <param name="dbID">Unique ID generated for the Database by Azure</param>
        /// <param name="collectionID">Unique ID generated for the Collection by Azure</param>
        /// <param name="documentID">Unique ID generated for the Document by Azure</param>
        /// <returns>A Json file containing the documentStructure 
        /// OR a message that indicates the failure of the request. 
        /// More info on status code at https://msdn.microsoft.com/en-us/library/azure/dn803957.aspx </returns>
        public static async Task<string> getDocument(string accountID, string dbID, string collectionID, string documentID)
        {
            string url = "https://{0}.documents.azure.com/dbs/{1}/colls/{2}/docs/{3}";
            url = String.Format(url, accountID, dbID, collectionID,documentID);
            HttpClient client = await GetClient("get", accountID, documentID);

            try
            {
                string result = await client.GetStringAsync(url);
                return result;
            }
            catch (Exception ex) { return ex.Message; }
        }

        /// <summary>
        /// Async method that Deletes a Document given it's Unique ID assigned by Azure 
        /// when the Document was created
        /// </summary>
        /// <param name="accountID">Document DB Account name</param>
        /// <param name="dbID">Unique ID generated for the Database by Azure</param>
        /// <param name="collectionID">Unique ID generated for the Collection by Azure</param>
        /// <param name="documentID">Unique ID generated for the Document by Azure</param>
        /// <returns>A message that indicates the success or failure of the request. 
        /// More info on status code at https://msdn.microsoft.com/en-us/library/azure/dn803952.aspx </returns>
        public static async Task<string> deleteDocument(string accountID, string dbID, string collectionID, string documentID)
        {
            string url = "https://{0}.documents.azure.com/dbs/{1}/colls/{2}/docs/{3}";
            url = String.Format(url, accountID, dbID, collectionID, documentID);
            HttpClient client = await GetClient("delete", accountID, documentID);

            try
            {
                HttpResponseMessage result = await client.DeleteAsync(url);
                return result.StatusCode.ToString() + " Documento eliminado";
            }
            catch (Exception ex) { return ex.Message; }
        }

        /// <summary>
        /// Async method that Updates a Document given it's Unique ID 
        /// assigned by Azure when the Document was created
        /// </summary>
        /// <param name="accountID">Document DB Account name</param>
        /// <param name="dbID">Unique ID generated for the Database by Azure</param>
        /// <param name="collectionID">Unique ID generated for the Collection by Azure</param>
        /// <param name="documentID">Unique ID generated for the Document by Azure</param>
        /// <param name="jsonFile">Json formated string that contains the new version of the Document 
        /// that will replace the Json file hosted in DocumentDB</param>
        /// <returns>A message that indicates the success or failure of the request. 
        /// More info on status code at https://msdn.microsoft.com/en-us/library/azure/dn803947.aspx </returns>
        public static async Task<string> updateDocument(string accountID, string dbID, string collectionID, string documentID, string jsonFile)
        {
            string url = "https://{0}.documents.azure.com/dbs/{1}/colls/{2}/docs/{3}";
            url = String.Format(url, accountID, dbID, collectionID, documentID);

            StringContent body = GetStringContent(jsonFile);
            HttpClient client = await GetClient("put", accountID, documentID);

            try
            {
                HttpResponseMessage result = await client.PutAsync(url, body);
                return result.StatusCode.ToString() + " Documento actualizado";
            }
            catch (Exception ex) { return ex.Message; }
        }
    }
}
