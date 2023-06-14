using ScheduleAppLogic;
using ScheduleAppLogic.ModelInterfaces;
using ScheduleWebAPI;
using DBGateWay;

var builder = WebApplication.CreateBuilder ( args );

// Add services to the container.
builder.Services.AddControllers ( opts => { opts.ModelBinderProviders.Insert ( 0 , new CustomDateTimeModelBinderProvider ( ) ); } );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer ( );
builder.Services.AddSwaggerGen ( );

SetDomainLogicInsideImplementations ( builder );
PerformDataPreload ( builder );

var app = builder.Build ( );

// Configure the HTTP request pipeline.
if ( app.Environment.IsDevelopment ( ) )
{
    app.UseSwagger ( );
    app.UseSwaggerUI ( );
}

app.UseHttpsRedirection ( );

app.UseAuthorization ( );

app.MapControllers ( );

app.Run ( );



static void SetDomainLogicInsideImplementations ( WebApplicationBuilder builder )
{
    builder.Services.AddSingleton ( new ServiceDescriptor ( typeof ( IPublicAbstractionImplementationProvider ) ,
                                                            new ImplementationProvider ( builder.Services ) ) );

    builder.Services.AddSingleton ( new ServiceDescriptor ( typeof ( IAppInsideAbstractionImplementationProvider ) ,
                                                                     new AppInsideImplementationProvider ( ) ) );

    builder.Services.AddSingleton<IServiceProvider , PreparersStorageFactory> ( );

    builder.Services.AddSingleton<ILoginLogic , LoginLogic>
                             ( ( factory ) => { return ( LoginLogic ) factory.GetService ( typeof ( LoginLogic ) ); } );

    builder.Services.AddSingleton<IRegisterLogic , RegisterLogic>
                             ( ( factory ) => { return ( RegisterLogic ) factory.GetService ( typeof ( RegisterLogic ) ); } );

    builder.Services.AddSingleton<IClientsStorageDbGateway , ClientsStorageDbGateway>
                             ( ( factory ) => { return ( ClientsStorageDbGateway ) factory.GetService ( typeof ( ClientsStorageDbGateway ) ); } );

    builder.Services.AddSingleton<IRegisterDBGateway , RegistrationDBGateway>
                             ( ( factory ) => { return ( RegistrationDBGateway ) factory.GetService ( typeof ( RegistrationDBGateway ) ); } );

    builder.Services.AddSingleton<IDataEditionLogic , DataEdition>
                             ( ( factory ) => { return ( DataEdition ) factory.GetService ( typeof ( DataEdition ) ); } );

    builder.Services.AddSingleton<IPreparersGetterAndSaverDbGateway , SavingPreparersDbGateway>
                             ( ( factory ) => { return ( SavingPreparersDbGateway ) factory.GetService ( typeof ( SavingPreparersDbGateway ) ); } );
}


static void PerformDataPreload ( WebApplicationBuilder builder )
{
    var serviceProvider = builder.Services.BuildServiceProvider ( );
    var causingLoadingStaticDataFromDb = serviceProvider.GetService<IClientsStorageDbGateway> ( );
}
