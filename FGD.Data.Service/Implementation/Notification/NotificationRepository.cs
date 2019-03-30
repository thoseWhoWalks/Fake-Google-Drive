using FGD.Bussines.Model;
using FGD.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FGD.Data.Service
{
    public class NotificationRepository : INotificationRepository<NotificationModelBussines<int>, int>
    {
        private FakeGoogleDriveContext _context;
         
        public NotificationRepository(FakeGoogleDriveContext context)
        {
            _context = context; 
        }

        public async Task<NotificationModelBussines<int>> CreateAsync(NotificationModelBussines<int> model)
        { 

            var res = await _context.Notifications.AddAsync(
                   AutoMapperConfig.Mapper.Map<NotificationModel<int>>(model)
               );

            await _context.SaveChangesAsync();

            return await GetByIdAsync(res.Entity.Id);
        }

        [Obsolete]
        public Task<bool> DeleteByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<NotificationModelBussines<int>>> GetAllAsync()
        {  
            return AutoMapperConfig.Mapper.Map<List<NotificationModelBussines<int>>>(
                await _context.Notifications.ToListAsync()
                );
        }

        public async Task<ICollection<NotificationModelBussines<int>>> GetAllByUserId(int Id)
        {
           
              var res = await _context.Notifications.Where(i => i.AccountId == Id).ToListAsync();
             
              return AutoMapperConfig.Mapper.Map<List<NotificationModelBussines<int>>>(res);
             
        }

        public async Task<NotificationModelBussines<int>> GetByIdAsync(int id)
        {
            var notification = await GetRawById(id);
            
            if (notification == null)
                return null;

            var mapped = AutoMapperConfig.Mapper.Map<NotificationModelBussines<int>>(notification); 
            
            return mapped;
        }

        public async Task<NotificationModelBussines<int>> UpdateAsync(int id, NotificationModelBussines<int> model)
        {
              
            var raw = await this.GetRawById(id);
              
            raw.IsDeleted = model.IsDeleted;

            raw.Title = model.Title??raw.Title;

            raw.Descritpion = model.Descritpion??raw.Descritpion;

            raw.NotificationState = model.NotificationState;
             
            _context.Notifications.Update(raw);
            
            await _context.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        internal async Task<NotificationModel<int>> GetRawById(int id) => await _context.Notifications.FirstOrDefaultAsync(st => st.Id == id);
         
    }
}
