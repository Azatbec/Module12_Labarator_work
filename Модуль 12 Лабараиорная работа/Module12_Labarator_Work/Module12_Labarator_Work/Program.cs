using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module12_Labarator_Work
{
    public interface IBookingState
    {
        void SelectRoom();
        void ConfirmBooking();
        void MakePayment();
        void CancelBooking();
    }

    public class BookingContext
    {
        public IBookingState CurrentState { get; set; }

        public BookingContext()
        {
            CurrentState = new IdleState(this);
        }

        public void SelectRoom() => CurrentState.SelectRoom();
        public void ConfirmBooking() => CurrentState.ConfirmBooking();
        public void MakePayment() => CurrentState.MakePayment();
        public void CancelBooking() => CurrentState.CancelBooking();

    }

    public class IdleState : IBookingState
    {
        private readonly BookingContext _context;

        public IdleState(BookingContext context)
        {
            _context = context;
        }

        public void SelectRoom()
        {
            Console.WriteLine("Выбрана комната. Переход в состояние RoomSelected");
            _context.CurrentState = new RoomSelectedState(_context);
        }

        public void ConfirmBooking() => Console.WriteLine("Невозможно подтвердить бронирование. Номер не выбран.");
        public void MakePayment() => Console.WriteLine("Невозможно произвести оплату. Бронирование не подтверждено.");
        public void CancelBooking() => Console.WriteLine("Нет возможности отменить бронирование.");
    }

    public class RoomSelectedState : IBookingState
    {
        private readonly BookingContext _context;

        public RoomSelectedState(BookingContext context)
        {
            _context = context;
        }

        public void SelectRoom() => Console.WriteLine("Номер уже выбран.");
        public void ConfirmBooking()
        {
            Console.WriteLine("Бронирование подтверждено. Переход в состояние «Бронирование подтверждено».");
            _context.CurrentState = new BookingConfirmedState(_context);


        }

        public void MakePayment() => Console.WriteLine("Невозможно произвести оплату. Бронирование не подтверждено.");
        public void CancelBooking()
        {
            Console.WriteLine("Бронирование отменено. Возвращаемся в состояние «Неактивно».");
            _context.CurrentState = new IdleState(_context);
        }
    }

    public class BookingConfirmedState : IBookingState
    {
        private readonly BookingContext _context;

        public BookingConfirmedState(BookingContext context)
        {
            _context = context;
        }

        public void SelectRoom() => Console.WriteLine("Невозможно выбрать номер. Бронирование уже подтверждено.");
        public void ConfirmBooking() => Console.WriteLine("Бронирование уже подтверждено.");
        public void MakePayment()
        {
            Console.WriteLine("Платеж прошел успешно. Переход в состояние «Оплачено».");
            _context.CurrentState = new PaidState(_context);
        }

        public void CancelBooking()
        {
            Console.WriteLine("Бронирование отменено. Возвращаемся в состояние «Неактивно».");
            _context.CurrentState = new IdleState(_context);
        }
    }
    public class PaidState : IBookingState
    {
        private readonly BookingContext _context;

        public PaidState(BookingContext context)
        {
            _context = context;
        }

        public void SelectRoom() => Console.WriteLine("Невозможно выбрать комнату. Бронирование уже завершено.");
        public void ConfirmBooking() => Console.WriteLine("Невозможно подтвердить бронирование. Бронирование уже завершено.");
        public void MakePayment() => Console.WriteLine("Оплата уже произведена.");
        public void CancelBooking() => Console.WriteLine("Невозможно отменить бронирование. Бронирование уже завершено.");
    }
    class Program
    {
        static void Main(string[] args)
        {
            BookingContext bookingContext = new BookingContext();

            // Начальное состояние
            bookingContext.SelectRoom(); // Переход в RoomSelected
            bookingContext.ConfirmBooking(); // Переход в BookingConfirmed
            bookingContext.MakePayment(); // Переход в Paid

            // Пытаемся отменить после оплаты
            bookingContext.CancelBooking(); // Ошибка
            Console.ReadKey();
        }
        
    }


}
