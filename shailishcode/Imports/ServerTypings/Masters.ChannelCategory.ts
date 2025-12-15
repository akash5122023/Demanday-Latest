namespace AdvanceCRM.Masters {
    export enum ChannelCategory {
        DirectCustomer = 1,
        CustomerFromChannel = 2,
        Reseller = 3,
        Wholesaler = 4,
        Dealer = 5,
        Distributor = 6,
        Stockist = 7,
        NationalDistributor = 8
    }
    Serenity.Decorators.registerEnumType(ChannelCategory, 'AdvanceCRM.Masters.ChannelCategory', 'Masters.ChannelCategory');
}
