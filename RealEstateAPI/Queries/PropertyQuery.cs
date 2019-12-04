using System.Collections.Generic;
using GraphQL.Types;
using RealEstate.Types.Property;
using RealEstate.DataAccess.Repositories.Contracts;
using RealEstate.Types.Payment;

namespace RealEstate.API.Queries
{
    public class PropertyQuery : ObjectGraphType
    {
        public PropertyQuery(IPropertyRepository propertyRepository,IPaymentRepository paymentRepository)
        {
            Field<ListGraphType<PropertyType>>(
                "properties",
                resolve: context => propertyRepository.GetAll());

            Field<PropertyType>(
                "property",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => propertyRepository.GetById(context.GetArgument<int>("id")));

            Field<ListGraphType<PaymentType>>(
                "paymentForProperty",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "propertyId" }),

                resolve: context => paymentRepository.GetAllForProperty(propertyRepository.GetById(context.GetArgument<int>("propertyId")).Id));
        }
    }
}