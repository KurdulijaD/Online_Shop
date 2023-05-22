using AutoMapper;
using Online_Shop.Dto;
using Online_Shop.Interfaces.RepositoryInterfaces;
using Online_Shop.Interfaces.ServiceInterfaces;
using Online_Shop.Models;
using Online_Shop.Repository;
using System.Text;

namespace Online_Shop.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public OrderService(IOrderRepository repository, IMapper mapper, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<OrderDto> CreateOrder(OrderDto orderDto)
        {
            Order newOrder = _mapper.Map<Order>(orderDto);

            if (String.IsNullOrEmpty(newOrder.Address))
                throw new Exception($"You must fill field for address!");

            newOrder.OrderTime = DateTime.Now;
            newOrder.DeliveryTime = new Random().Next(1, 13);
            newOrder.DeliveryPrice = 200;
            newOrder.Status = Common.EOrderStatus.INPROGRESS;

            //foreach(OrderProduct op in newOrder.OrderProducts)
            //{
            //    newOrder.Price += op.Price * op.Amount;
            //}

            OrderDto dto = _mapper.Map<OrderDto>(await _repository.CreateOrder(newOrder));
            return dto;
        }

        public async Task<OrderDto> DenieOrder(int id)
        {
            Order o = await _repository.DenieOrder(id);
            if (o == null)
                throw new Exception($"Order with ID: {id} doesn't exist.");
            return _mapper.Map<OrderDto>(o);
        }

        public async Task<List<OrderDto>> GetAllInProgressOrders()
        {
            List<Order> orders = await _repository.GetAllInProgressOrders();
            if (orders == null)
                throw new Exception($"There are no orders in progress!");
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<List<OrderDto>> GetAllOrders()
        {
            List<Order> orders = await _repository.GetAllOrders();
            if (orders == null)
                throw new Exception($"There are no orders!");
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderById(int id)
        {
            Order o = await _repository.GetOrderById(id);
            if (o == null)
                throw new Exception($"Order with ID: {id} doesn't exist.");
            return _mapper.Map<OrderDto>(o);
        }
    }
}
