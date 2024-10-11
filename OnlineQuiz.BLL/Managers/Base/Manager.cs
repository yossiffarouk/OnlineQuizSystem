using AutoMapper;
using OnlineQuiz.DAL.Repositoryies.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineQuiz.BLL.Managers.Base
{
    public class Manager<T, U> : IManager<T, U> where T : class
    {
        private readonly IRepository<T, U> _repository;
        private readonly IMapper _mapper;

        public Manager(IRepository<T, U> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Get all entities as IQueryable, applying AutoMapper for DTO mapping if needed
        public IQueryable<T> GetAll()
        {
            return _repository.GetAll();
        }

        // Get a single entity by ID, mapping to DTO if necessary
        public T GetById(U id)
        {
            return _repository.GetById(id);
        }

        // Add a new entity (DTO to entity mapping)
        public void Add(T entity)
        {
            _repository.Add(entity);
        }

        // Update an existing entity
        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        // Delete an entity by ID
        public void DeleteById(U id)
        {
            _repository.DeleteById(id);
        }
    }
}
