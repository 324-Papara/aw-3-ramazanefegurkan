﻿using MediatR;
using Para.Base.Response;
using Para.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Para.Bussiness.Cqrs;

public record CreateCustomerPhoneCommand(CustomerPhoneRequest Request) : IRequest<ApiResponse<CustomerPhoneResponse>>;
public record UpdateCustomerPhoneCommand(long CustomerPhoneId, CustomerPhoneRequest Request) : IRequest<ApiResponse>;
public record DeleteCustomerPhoneCommand(long CustomerPhoneId) : IRequest<ApiResponse>;

public record GetAllCustomerPhoneQuery() : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
public record GetCustomerPhoneByIdQuery(long CustomerPhoneId) : IRequest<ApiResponse<CustomerPhoneResponse>>;
public record GetAllCustomerPhoneByCustomerIdQuery(long CustomerId) : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;
public record GetCustomerPhoneByParametersQuery(long? CustomerPhoneId,long? CustomerId, string? CountyCode, string? Phone, bool? IsDefault) : IRequest<ApiResponse<List<CustomerPhoneResponse>>>;

