using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Olga.BLL.DTO;
using Olga.BLL.Interfaces;
using Olga.DAL.EF;
using Olga.DAL.Entities;
using Olga.DAL.Interfaces;
using Olga.DAL.Repositories;

namespace Olga.BLL.Services
{
    public class ProcedureService : IProcedure
    {
        IUnitOfWorkGeneral Database { get; set; }

        public ProcedureService(IUnitOfWorkGeneral uow)
        {
            Database = uow;
        }

        public void AddItem(ProcedureDTO item)
        {
            Procedure procedure = Mapper.Map<ProcedureDTO, Procedure>(item);
            Database.Procedures.Create(procedure);
        }

        public ProcedureDTO GetItem(int id)
        {
            return Mapper.Map<Procedure, ProcedureDTO>(Database.Procedures.Get(id));
        }

        public void DeleteItem(int id)
        {
            Database.Procedures.Delete(id);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public void Commit()
        {
            Database.Procedures.Commit();
        }

        public IEnumerable<ProcedureDTO> GetItems()
        {
            var procedures = Database.Procedures.GetAll().ToArray();
            return Mapper.Map<IEnumerable<Procedure>, IEnumerable<ProcedureDTO>>(procedures);
        }

        public IEnumerable<ProcedureDTO> GetItems(int productId)
        {
            return Mapper.Map<IEnumerable<Procedure>, IEnumerable<ProcedureDTO>>(Database.Procedures.GetAll(productId).ToArray());
        }

        public void Update(ProcedureDTO procedureDto)
        {
            var procedure = Mapper.Map<ProcedureDTO, Procedure>(procedureDto);
            Database.Procedures.Update(procedure);
        }

        //public void DeleteDocument(ProcedureDTO procedureDto)
        //{
        //    var procedure = Mapper.Map<ProcedureDTO, Procedure>(procedureDto);
        //    Database.Procedures.Update(procedure);
        //}

        public void DeleteDocument(string pathToDocument)
        {
            if (string.IsNullOrEmpty(pathToDocument)) return;
            Database.Procedures.DeleteDocument(pathToDocument);
        }


        public IEnumerable<ProcedureDTO> GetPaginated(int? countryId, string searchValue, string sortOrder, int initialPage, int pageSize, out int totalRecords, out int recordsFiltered)
        {
            var productIds = Database.Products.GetAll().Where(p => p.CountryId == countryId).Select(a => a.Id).ToList();

            var procedures = Database.Procedures.GetAll().Where(p => productIds.Contains(p.ProductId));

            totalRecords = procedures.Count();

            procedures = GetSearchedProcedures(procedures, searchValue);
            //procedures = GetSortedProcedures(procedures, sortOrder);

            recordsFiltered = procedures.Count();
            //procedures = procedures.Skip(initialPage * pageSize).Take(pageSize);

            var proceduresDTO = Mapper.Map<IEnumerable<Procedure>, IEnumerable<ProcedureDTO>>(procedures.ToArray());
            return proceduresDTO;
        }

        public IEnumerable<ProcedureDTO> GetProceduresOptimized(int? countryId, string searchValue,
            string sortColumnName, string sortDirection, int start, int length, out int totalrows, out int totalrowsafterfiltering)
        {
            var productIds = Database.Products.GetAll().Where(p => p.CountryId == countryId).Select(a => a.Id).ToList();
            var procedures = Database.Procedures.GetAll().Where(p => productIds.Contains(p.ProductId));
            totalrows = procedures.Count();
            //filter
            procedures = GetSearchedProcedures(procedures, searchValue);
            totalrowsafterfiltering = procedures.Count();
            //sorting
            procedures = GetSortedProcedures(procedures, sortColumnName, sortDirection);
            //paging
            procedures = procedures.Skip(start).Take(length);

            //procedures = procedures.Skip(initialPage * pageSize).Take(pageSize);

            var proceduresDTO = Mapper.Map<IEnumerable<Procedure>, IEnumerable<ProcedureDTO>>(procedures.ToArray());
            return proceduresDTO;
        }

        private IQueryable<Procedure> GetSearchedProcedures(IQueryable<Procedure> procedures, string searchValue)
        {
            if (!string.IsNullOrEmpty(searchValue))
            {
                procedures = procedures.Where(a => a.Name.ToLower().Contains(searchValue.ToLower())
                || a.ProcedureType.ToString().ToLower().Contains(searchValue.ToLower())
                || (!string.IsNullOrEmpty(a.Product.ProductName.Name) && a.Product.ProductName.Name.Contains(searchValue.ToLower()))
                || a.SubmissionDate.ToString().ToLower().Contains(searchValue.ToLower())
                || a.EstimatedSubmissionDate.ToString().ToLower().Contains(searchValue.ToLower())
                || (!string.IsNullOrEmpty(a.Product.ProductCode.Code) && a.Product.ProductCode.Code.ToString().ToLower().Contains(searchValue.ToLower()))
                || (!string.IsNullOrEmpty(a.Product.ProductName.Name) && a.Product.ProductName.Name.ToString().ToLower().Contains(searchValue.ToLower())));
            }
            return procedures;
        }
        private IQueryable<Procedure> GetSortedProcedures(IQueryable<Procedure> procedures, string sortColumnName, string sortDirection)
        {
            switch (sortColumnName)
            {
                case "Name":
                    procedures = sortDirection.Equals("asc") ? procedures.OrderBy(a => a.Name) : procedures.OrderByDescending(a => a.Name);
                    break;
                case "ProcedureType":
                    procedures = sortDirection.Equals("asc") ? procedures.OrderBy(a => a.ProcedureType) : procedures.OrderByDescending(a => a.ProcedureType);
                    break;
                case "SubmissionDate":
                    procedures = sortDirection.Equals("asc") ? procedures.OrderBy(a => a.SubmissionDate) : procedures.OrderByDescending(a => a.SubmissionDate);
                    break;
                case "ApprovalDate":
                    procedures = sortDirection.Equals("asc") ? procedures.OrderBy(a => a.ApprovalDate) : procedures.OrderByDescending(a => a.ApprovalDate);
                    break;
                case "EstimatedApprovalDate":
                    procedures = sortDirection.Equals("asc") ? procedures.OrderBy(a => a.EstimatedApprovalDate) : procedures.OrderByDescending(a => a.EstimatedApprovalDate);
                    break;
                case "EstimatedSubmissionDate":
                    procedures = sortDirection.Equals("asc") ? procedures.OrderBy(a => a.EstimatedSubmissionDate) : procedures.OrderByDescending(a => a.EstimatedSubmissionDate);
                    break;
                default:
                    procedures = sortDirection.Equals("asc") ? procedures.OrderBy(a => a.ProcedureType) : procedures.OrderByDescending(a => a.ProcedureType);
                    break;
            }
            return procedures;
        }

        public async Task AddDocumentToArchive(int documentId, bool toArchive)
        {
            await Database.Procedures.ArchiveDocumentAsync(documentId, toArchive);
        }

    }
}
