using DocumentSystem.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentSystem.ViewModel
{
    public class Upload_DocumentmModel
    {
        public List<TDocumentDetails> upload_Documents { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
    public class ListOfDocs
    {
        public List<GetUserDocumentModel> GetUserDocumentModel { get; set; }
    }
    public class GetUserDocumentModel
    {
        public TDocumentDetails upload_Documents { get; set; }
        public long DocumentRecordid { get; set; }
        public long? FromId { get; set; }
        public string FromUser { get; set; }
        public long? ToId { get; set; }
        public string ToUser { get; set; }
        public string Comment { get; set; }
        public bool? IsDelete { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? DeletedBy { get; set; }
    }

    public class UserDocumentsResponseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Base64 { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentTypeName { get; set; }
        public string DocumentTypeNameStaff { get; set; }
        public DateTime? Expiration { get; set; }
        public String OtherDocumentType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public MemoryStream File { get; set; }
        public string Extenstion { get; set; }
        public string FileName { get; set; }
    }
}
