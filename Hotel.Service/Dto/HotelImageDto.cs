﻿using System;

namespace Hotel.Infrastructure.ViewModel.Dto
{
    public class HotelImageDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int HotelId { get; set; }
        public DateTime CreateDate { get; set; }
    }

}
