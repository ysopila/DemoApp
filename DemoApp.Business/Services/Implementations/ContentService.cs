using System.Collections.Generic;
using System.Linq;
using DemoApp.Business.Models;
using DemoApp.Core;
using DemoApp.Business.Services.Abstractions;
using DemoApp.DataModel.Interfaces;
using DemoApp.Core.Mapper;
using Entity = DemoApp.DataModel.Entities;

namespace DemoApp.Business.Services.Implementations
{
	public class ContentService : IContentService
	{
		private IUnitOfWork _unitOfWork;
		private ISimpleMapper _mapper;

		public ContentService(IUnitOfWork unitOfWork, ISimpleMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public IEnumerable<Content> GetAll()
		{
			return _unitOfWork.ContentRepository.Find().Map<Entity.ContentObject, Content>(_mapper.Map<Entity.ContentObject, Content>);
		}

		public Content Get(int id)
		{
			return _mapper.Map<Entity.ContentObject, Content>(_unitOfWork.ContentRepository.Find(filter: x => x.Id == id).SingleOrDefault());
		}

		public Content Add(Content model)
		{
			var entity = _mapper.Map<Content, Entity.ContentObject>(model);
			_unitOfWork.ContentRepository.Insert(entity);
			_unitOfWork.Save();
			return _mapper.Map<Entity.ContentObject, Content>(entity);
		}

		public Content Save(Content model)
		{
			var entity = _unitOfWork.ContentRepository.Find(filter: x => x.Id == model.Id).SingleOrDefault();
			entity.Description = model.Description;
			entity.Name = model.Name;
			entity.Photo = model.Photo;
			_unitOfWork.ContentRepository.Insert(entity);
			_unitOfWork.Save();
			return _mapper.Map<Entity.ContentObject, Content>(entity);
		}

		public void Delete(int id)
		{
			var entity = _unitOfWork.ContentRepository.Find(filter: x => x.Id == id).SingleOrDefault();
			_unitOfWork.ContentRepository.Delete(entity);
			_unitOfWork.Save();
		}
	}
}
