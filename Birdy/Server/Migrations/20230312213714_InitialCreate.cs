using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Birdy.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Birds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NutritionType = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true),
                    Size = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true),
                    ImageSource = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Birds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Url = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageSource = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    ImageSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NutritionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirdSize = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Logins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "date", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", maxLength: 250, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    IsPaid = table.Column<bool>(type: "bool", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(25)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    UserAddress = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    UserTelephone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BirdProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BirdId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BirdProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BirdProducts_Birds_BirdId",
                        column: x => x.BirdId,
                        principalTable: "Birds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BirdProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductIngredients",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductIngredients", x => new { x.ProductId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_ProductIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductIngredients_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    InStock = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPrices_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Raitings",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Raitings", x => new { x.ProductId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Raitings_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Raitings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => new { x.ProductId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Reviews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrders_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductPriceId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discounts_ProductPrices_ProductPriceId",
                        column: x => x.ProductPriceId,
                        principalTable: "ProductPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Birds",
                columns: new[] { "Id", "Description", "ImageSource", "Name", "NutritionType", "Size" },
                values: new object[,]
                {
                    { 1, "Среди множества видов декоративных птиц маленькие попугаи – самые востребованные. Такая популярность объясняется не только простотой содержания, но и количеством позитива, исходящего от этих пернатых. Миниатюрные птички забавны, миловидны и умны. Они прекрасные собеседники: всегда выслушают хозяина и даже «поддержат» разговор. Если вы хотите завести экзотическую птичку, обратите внимание на представителей маленьких видов: дятловых, воробьиных, фиговых, волнистых попугаев или неразлучников. За ними легко ухаживать. Малые размеры птиц – не единственное их положительное качество. Они легко приспосабливаются к жизни рядом с человеком.", "/images/birds/littleparrot.jpg", "Маленький попугай", "Растительноядные", "Маленький" },
                    { 2, "Многие люди в качестве домашних питомцев выбирают средних попугаев. Эти птички отличаются относительно небольшими размерами и красотой, благодаря чему их можно успешно содержать в городской малогабаритной квартире. Попугаи средних размеров насчитывают множество родов и видов. Самыми популярными экземплярами, живущими рядом с человеком, являются корелла, лори, розелла, кольчатый и сенегальский попугаи.", "/images/birds/mediumparrot.jpg", "Средний попугай", "Растительноядные", "Средний" },
                    { 3, "На сегодняшний день на планете живет около 350 видов попугаев. Из них крупные породы представлены в незначительном количестве. Некоторые из них находятся под угрозой вымирания и занесены в Красную книгу. Цены на таких питомцев высоки, но и спрос на них не угасает. Среди крупных попугаев наиболее известны: ара, амазонский попугай, какаду, какаду маффин.", "/images/birds/largeparrot.jpg", "Крупный попугай", "Растительноядные", "Крупный" },
                    { 4, "Канарейка птица маленькая. Благодаря своему умению красиво разливать трели, даже несмотря на скромное оперение, она завоевала популярность и любовь многих людей. Певунья не приносит неудобств, а только удовольствие от одного своего неприхотливого вида, незамысловатого щебетания, которое сменяется удивительным по звучанию пением. Клетка не занимает много места в квартире. За ней легко ухаживать и не нужно тратить время на прогулки с питомцем.", "/images/birds/canary.jpg", "Канарейка", "Растительноядные", "Маленький" },
                    { 5, " Амадины - это общительные птицы, которые любят взаимодействовать с людьми. Для содержания не требуют большие финансовые вложения и физические усилия. Благодаря такой неприхотливости в сочетании с миролюбивым характером, яркие певчие птички завоевали широкую популярность. «Пение» амадин специфичное и порой бывает неприятным: они издают тихое щебетание, свист, жужжание, бурчание, шипение. У некоторых амадин приятный голос, но поют только самцы. Привлекательные, подвижные, красивые и очень доверчивые, они моментально завоевывают сердца любителей птиц.", "/images/birds/amadina.jpg", "Амадина", "Растительноядные", "Маленький" },
                    { 6, "Голубь – птица мира, которая на протяжении сотни лет служит человеку и выступает в качестве друга. Еще в давние времена, когда не было почты, в качестве почтальонов выступали именно эти замечательные птицы. Они доставляли письма адресату и возвращались домой к хозяину. Голубей любят и ценят в нынешние дни, некоторые люди мечтают завести крылатого ангела у себя дома, в квартире в том числе. Домашние голуби способны приносить эстетическое удовлетворение своим ярким внешним видом или необычным стилем полета.", "/images/birds/pigeon.jpg", "Голубь", "Растительноядные", "Средний" },
                    { 7, "Ворон – это символичный, для некоторых даже эпичный представитель пернатых. Многие орнитологи считают врановых самыми умными из птиц: они способны различать цвета и формы, запоминают запахи, реагируют на тембр голоса, имеют навыки к подражанию звуков. Ручной ворон отличается от сородичей невероятным интеллектом, нетипичным для птиц. Он кардинально отличается от попугаев и других пернатых, а еще обладает невероятной привязанностью, как верный пес.", "/images/birds/raven.jpeg", "Ворон", "Всеядные", "Крупный" },
                    { 8, "Пожалуй, ни одной птице не посвящено столько стихов и произведений, как соловью. И ведь неспроста. Его пение поражает разнообразием репертуара, силой звука и голоса. При этом совершенно удивительно после услышанного восхитительного исполнения увидеть этого невзрачного артиста. Соловей не привлекает внешними данными, зато изумляет его певческое мастерство. К тому же, соловьиные песни возвещают о приходе весны и цветении природы вокруг, от чего на душе становится теплей.", "/images/birds/nightingale.jpg", "Соловей", "Насекомоядные", "Маленький" },
                    { 9, "Дрозды – недоверчивые, нервные и пугливые птицы, хотя часто можно встретить и спокойных особей с уравновешенным характером. Они также непоседливы, умны и активны. В домашних условиях любители птиц содержат, как правило, певчего дрозда, хотя среди лучших певцов, обитающих в лесах России, можно назвать чёрного и сизого дрозда, а также очень нарядных и голосистых птиц из группы каменных дроздов.", "/images/birds/blackbird.jpeg", "Дрозд", "Насекомоядные", "Средний" },
                    { 10, "Скворец – это искусный звукоподражатель, он может имитировать самые разные звуки: мяуканье котов, кваканье жаб, крик ласточек, петухов, куриц, уток и многое другое. Он прекрасно поёт, легко приручается и охотно общается с человеком. Если вы видите скворца возле вашего дома, то это не редкость, так как скворцы обитают в городских парках, садах и около домов.", "/images/birds/starling.jpg", "Скворец", "Всеядные", "Средний" },
                    { 11, "Сова, даже домашняя, – хищная птица, которая имеет острый клюв и когти. Это милый пушистик под покровом ночи насквозь протыкает свою жертву когтями и раздирает клювом. Совы по природе очень умные и хитрые, взмах их крыла совершенно бесшумен, что позволяет максимально приблизиться к жертве не вызывая подозрений. Мощные сильные лапы способны разорвать добычу, которая не превышает размера самой птицы. Поэтому, если в вашем доме имеется хомяк, кот или маленькая собака, не рекомендуется заводить хищную птицу, так как любимцы легко могут стать ее жертвой.", "/images/birds/owl.jpg", "Сова", "Плотоядные", "Средний" },
                    { 12, "Зяблики - одна из самых распространенных певчих птиц в средней полосе России, и неудивительно, что при этом они являются одними из самых популярных в качестве домашних любимцев. Однако стоит отметить, что хоть зяблики и приживаются в неволе, по-настоящему ручными они все-таки не становятся. И это требует от владельца определенного опыта в содержании птиц, что в итоге делает зябликов птицей не для новичков.", "/images/birds/finch.jpg", "Зяблик", "Растительноядные", "Маленький" },
                    { 13, "Синица в домашних условиях может жить долго и счастливо. Главное, подобрать для нее правильный рацион. Она не боится человека, легко приучается к жизни в неволе, красиво поёт и неприхотлива в содержании. Они не боятся проживать по соседству с человеком и отлично себя чувствуют на приусадебных участках, в садах, городских скверах и лесопарковых зонах.", "/images/birds/titmouse.jpg", "Синица", "Насекомоядные", "Маленький" },
                    { 14, "Жаворонки пугливые, настороженные птицы, плохо поддающиеся приручению. Жаворонки, привыкшие к дому и владельцам, будут пугаться незнакомых людей. Эти птицы подходят для опытных любителей певчих птиц. При содержании в домашних условиях необходимо использовать несколько клеток разного типа. Жаворонки не только отличные певцы, но и превосходные «летуны». Такое свойство птицам данного семейства подарила сама природа - п0ри относительно небольшом туловище обладают огромными крыльями, коротким хвостом.", "/images/birds/skylark.jpg", "Жаворонок", "Растительноядные", "Маленький" },
                    { 15, "В домашних условиях большинство коноплянок остаются пугливыми птицами, часто бьются о прутья клетки при резких движениях человека или домашних животных. Тем не менее их часто содержат в клетках из-за красивого пения. При вольерном содержании иногда дают потомство, получены гибриды с канарейками, щеглами и зеленушками. Гибриды с канарейками отличаются неплохими голосовыми данными и даже во взрослом состоянии способны совершенствовать песню, перенимая её у поющих кенаров.", "/images/birds/linnet.jpg", "Коноплянка", "Растительноядные", "Маленький" },
                    { 16, "Галка неприхотлива в неволе, как и другие врановые, мирится с вегетарианским столом. Кроме того, характер у нее относительно спокойный. Галка — единственная из врановых птиц, голос которой не лишен мелодичности. Жаль, что галка несколько «глупее» вороны и даже сороки и грача. Она плохо разбирает врагов и друзей, идет иногда на зов к незнакомому человеку. ", "/images/birds/jackdaw.jpeg", "Галка", "Всеядные", "Средний" },
                    { 17, "Фазаны в домашнем хозяйстве встречаются не часто. Однако в последние годы в России к этому направлению значительно активизировался интерес. Птиц используют не только для получения диетического мяса и яйца. Многие состоятельные люди держат этих великолепных по красоте птиц в качестве декоративных. Фазаны горделиво разгуливают по саду, словно маленькие павлины. От их нарядного оперения невозможно оторвать взгляд.", "/images/birds/pheasant.jpg", "Фазан", "Всеядные", "Крупный" },
                    { 18, "Снегири – это не только красивые птицы, но и удивительные создания, способные легко запоминать и воспроизводить мелодии и звуки. Что касается характера, то они отличаются уравновешенностью. Эти птицы спокойны, они никуда не торопятся, аккуратны и осмотрительны. Снегири неплохо живут в соседстве с людьми, неволю они переносят хорошо. Только из-за снижения количества полетов начинают полнеть. ", "/images/birds/bullfinch.jpg", "Снегирь", "Растительноядные", "Маленький" },
                    { 19, "Воробьи – общительные, дружелюбные птички. Живут в тесном соседстве с животными, другими пернатыми и человеком. Правда шумные и суетливые по природе, не всегда уживаются со стрижами и синицами. Известно, что у воробьев развита память. Доказано, что они могут составлять логическую цепочку из событий. Боятся кошек, но любят дразнить их, прыгая на коротких лапках у ее миски.", "/images/birds/sparrow.jpg", "Воробей", "Растительноядные", "Маленький" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "Url" },
                values: new object[,]
                {
                    { 1, "Переноски для птиц", "carriers" },
                    { 2, "Корм", "food" },
                    { 3, "Кормушки", "feeders" },
                    { 4, "Лакомства", "goodies" },
                    { 5, "Средства по уходу", "careproducts" },
                    { 6, "Клетки", "cells" },
                    { 7, "Игрушки", "toys" },
                    { 8, "Наполнители", "fillers" },
                    { 9, "Аксессуары", "accessories" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Description", "ImageSource", "Name" },
                values: new object[,]
                {
                    { 1, "Наиболее широко распространенный вид корма для большинства растительноядных птиц и всех видов грызунов. Как основной компонент входит в состав большинства питательных смесей. Обладает хорошей всхожестью и может быть использован для подкормки животных зеленью, особенно в осенне-зимний период. Для полноценного питания птиц в неволе необходим пророщенный овес.", "/images/ingredients/oats.jpg", "Овес" },
                    { 2, "Просо - питательный злак, широко используемый для всех декоративных зерноядных птиц и грызунов, ценный корм для птицы, богатый каротином и аминокислотами. Просо служит основой для составления различных питательных зерновых смесей, его доля в рационе птиц достигает 60-70%. Содержит много клетчатки и белка, при этом не содержит глютена и является самым щелочным злаковым - легче переносится желудочно-кишечным трактом. Сухой корм для волнистых попугайчиков, средних попугаев и канареек обязательно должен содержать просо.", "/images/ingredients/millet.jpg", "Просо" },
                    { 3, "Канареечное семя наиболее подходящий корм для канареек. Канареечник в диком виде растет на Канарских островах. Семена этого злакового растения завезены в Европу вместе с канарейками. Сейчас их возделывают в некоторых странах на юге Европы, где широко используют в качестве корма для птиц. Канареечное семя включает триптофан и помогает птицам справляться со стрессом. Его количество в составе кормов может доходить до 15%.", "/images/ingredients/canaryseed.jpg", "Канареечное семя" },
                    { 4, "Конопляное семя — любимый корм всех зерноядных птиц. Зерна конопли содержат полиненасыщенные жирные кислоты. В небольшом количестве они очень полезны для иммунитета, пищеварения, оперения. Но если давать их птице слишком много, то вскоре придется вводить ограничения в рацион питания из-за развившегося ожирения. Более того, чрезмерное употребление конопляного семени может привести к опухоли век, слепоте и даже к гибели птицы.", "/images/ingredients/hempseed.jpg", "Конопляное семя" },
                    { 5, "Рапс может быть полезным и питательным источником пищи для птиц, если используется правильно. Белки рапса хорошо сбалансированы по аминокислотному составу, по содержанию лизина приближаются к сое, а по метионину и цистину, кальцию и фосфору значительно превосходят ее. Однако, перед тем как использовать рапс необходимо учитывать некоторые факторы. Во-первых, рапс может содержать глюкозинолаты, которые могут быть вредными для некоторых видов птиц, если они употребляются в больших количествах. Во-вторых, рапс может быть токсичным, если он загнивает или становится жирным. Это может произойти, если рапс не хранится должным образом или если он используется в слишком больших количествах. Поэтому необходимо следить за качеством и количеством рапса в рационе птиц.", "/images/ingredients/rapeseed.jpg", "Рапс" },
                    { 6, "Кунжут — одно из лучших масличных растений, культивируемое от западных берегов Африки вплоть до Китая и Японии, а также в Америке. Семена кунжута являются лидером по содержанию кальция. Кроме того, кунжутное семя богато витаминами В1 и Е, минералами и полиненасыщенными жирными линолевыми кислотами. Кунжут незаменим для дополнительного поддержания сил организма в период стресса, линьки у птиц. Однако, не следует давать птицам слишком много кунжута, так как это может привести к дисбалансу питательных веществ в рационе. Также следует учитывать, что кунжут может быть довольно жирным, поэтому его нужно вводить постепенно в рацион птиц, начиная с небольшого количества.", "/images/ingredients/sesame.jpeg", "Кунжут" },
                    { 7, "Подсолнечник — очень питательный вид корма. Однако подсолнечник не следует включать в больших количествах, так как избыток маслянистых веществ может вызвать нарушения обмена веществ. Очень полезны для птиц белые семена подсолнечника, так как они содержат меньше жиров. Полосатые семена богаты ненасыщенными жирными кислотами, которые благотворно влияют на оперение птиц.", "/images/ingredients/sunflower.jpg", "Подсолнечник" },
                    { 8, "Пшеница является богатым источником углеводов и белков, что делает ее хорошим источником энергии и питательных веществ для птиц. Однако, необходимо учитывать, что пшеница может быть довольно бедной по содержанию аминокислот, витаминов и минералов. Поэтому, если пшеница является основным источником пищи, то необходимо дополнительно добавлять другие виды кормов и/или дополнительные пищевые добавки, чтобы обеспечить птицам все необходимые питательные вещества. Кроме того, необходимо учитывать, что пшеница может быть довольно трудноусвояемой для молодых птиц и некоторых пород. Поэтому, если у вас есть молодые птицы или птицы с проблемами пищеварения, то необходимо убедиться, что они могут правильно усваивать пшеницу. В целом, пшеница может быть полезным источником пищи для птиц, если она используется в сочетании с другими видами кормов и дополнительными пищевыми добавками.", "/images/ingredients/wheat.jpg", "Пшеница" },
                    { 9, "Ячмень является одной из наиболее распространенных зерновых культур, используемых в качестве корма для птиц. В зависимости от вида птицы и ее возраста, ячмень может использоваться как основной источник питательных веществ или в комбинации с другими зерновыми культурами в кормовых смесях. Он содержит богатый набор белков, углеводов, клетчатки, минералов, витаминов и аминокислот, таких как лизин и треонин, которые важны для роста и развития птиц. Однако ячмень содержит фитиновую кислоту, которая может затруднять усвоение некоторых питательных веществ, таких как кальций, железо и цинк. Чтобы уменьшить воздействие фитиновой кислоты, ячмень может быть обработан термически или использован в сочетании с другими кормовыми ингредиентами, такими как мясо-костная мука или фосфаты.", "/images/ingredients/barley.jpg", "Ячмень" },
                    { 10, "Рожь – корм, используемый, когда недостает другого злакового зерна. Если переборщить с рожью, у пернатых начинаются проблемы с пищеварением. Это объясняется сильным набуханием крахмала ржи. Нельзя кормить пернатых недавно убранным зерном – провоцирует недуги ЖКТ. Зерно выдерживается в течение трех месяцев. Как и с другими зерновыми культурами, рожь может быть обработана термически или использована в сочетании с другими кормовыми ингредиентами, чтобы снизить уровень антипитательных веществ.", "/images/ingredients/rye.jpg", "Рожь" },
                    { 11, "Кукуруза - один из самых лучших и широко используемых видов кормов для декоративных зерноядных птиц. По энергетической ценности кукуруза превосходит многие другие зерновые корма, содержит много белка (9-12%), углеводов (65-70%) и мало - клетчатки. Богата железом и каротином (провитамином А). Особенно много провитамина А в желтозерных сортах. Кукуруза является одним из основных компонентов для составления различных зерновых смесей. Многие птицы неохотно едят сухую кукурузу, выклевывая лишь сердцевину. В этом случае рекомендуется зерно замачивать или запаривать.", "/images/ingredients/corn.jpg", "Кукуруза" },
                    { 12, "Сорго - это питательная пища, не проигрывающая по калорийности кукурузе и ячменю. В зерне почти в 10 раз выше содержание кальция, натрия и магния в сравнении с другими культурами, без вреда может занимать до 50% рациона несушек. Это способствует насыщению организма птицы минералами и формированию яичной скорлупы. Злаковые корма — основа калорийного питания уток и голубей, сорго охотно поедается ими и хорошо переваривается. Питательная ценность выше, чем у овса. Сорго - разновидность проса, поэтому попугаи с удовольствием его едят.", "/images/ingredients/sorghum.jpg", "Сорго" },
                    { 13, "Льняное семечко может использоваться в качестве дополнительного источника питательных веществ в корму для птиц. В семечках содержится большое количество лигнинов, которые отвечают за здоровье гормональной системы. Также семя содержит несколько важных для работы иммунитета и общего состояния организма жирных кислот и белок, который обладает особым строением и очень быстро усваивается организмом. Альфа-линолиевая кислота предупреждает развитие опухолей, стимулирует рост оперения и укрепляет иммунную систему.", "/images/ingredients/flaxseed.jpg", "Льняное семечко" },
                    { 14, "Соевые корма высоко ценятся из-за своего состава, большую часть которого занимает протеин, остальную – жиры, аминокислоты, витамины группы В, минералы, фосфор. Питательный продукт улучшает здоровье птиц, увеличивает продуктивность, несушки откладывают крупные яйца с крепкой скорлупой, мясные породы быстрее набирают вес. Наличие аминокислот ускоряет метаболизм в тканях. Но в свежих соевых продуктах имеются антипитательные (токсичные) вещества, поэтому не рекомендуется давать птице цельное зерно в большом объеме и зеленую часть бобовых. Накапливаясь в организме, токсины замедляют рост, отрицательно влияют на продуктивность и даже провоцируют летальный исход.", "/images/ingredients/soy.jpeg", "Соя" },
                    { 15, "Гречка, гречиха, или греча – достаточно широко распространенный на территории России продукт. Она богата белками, микроэлементами и аминокислотами, поэтому гречневая крупа и ее производные активно применяются во всех сферах пищевой промышленности: начиная от производства детского питания и заканчивая изготовлением кормов для животных. Для корма скота и домашней птицы чаще всего приобретают отруби и мучную пыль, которые отсеиваются в процессе обработки зерен.", "/images/ingredients/buckwheat.jpg", "Гречиха" },
                    { 16, "Орехи особенно популярны в качестве зимнего корма для птиц, потому что они долговечны и их легко спрятать для последующего использования. Многие птицы, которые питаются орехами, проводят недели осенью, запасая орехи в полостях коры, небольших углублениях в земле или в других укрытиях. Когда других источников пищи не хватает или плохая погода не позволяет правильно добывать пищу, птицы возвращаются в эти укрытия, чтобы съесть припасенные орехи. Но следует обращать внимание на их использование, так как не все виды орехов одинаково полезны для птиц, а некоторые могут быть вредными. Самыми распространенными орехами, которые могут использоваться в корму для птиц, являются грецкий орех, арахис, миндаль, кешью, фисташки.", "/images/ingredients/nuts.jpg", "Орехи" },
                    { 17, "Овощи могут быть важным источником питательных веществ в рационе птиц. Наиболее распространены: морковь (богата каротином, который может помочь поддерживать здоровье глаз и кожи у птиц), капуста (содержит много витамина С, кальция, витаминов и минералов), огурцы (богаты водой и могут помочь увлажнить корм птиц), брокколи (содержит много витаминов и минералов, включая витамины А, С и К, а также кальций и железо), тыква (содержит витамин А, калий и другие минералы). Кроме того, овощи, такие как листовые зеленые, помидоры, перец, редис, свекла и многие другие, могут также использоваться в корму для птиц. Важно помнить, что некоторые овощи могут быть токсичными для птиц, поэтому необходимо обращаться к опытным специалистам, чтобы определить, какие овощи лучше всего использовать в рационе для конкретного вида.", "/images/ingredients/vegetables.jpg", "Овощи" },
                    { 18, "Фрукты могут быть хорошим источником питательных веществ для птиц, но должны использоваться осторожно и только в умеренных количествах. Наиболее популярны: яблоки (источник витамина C, клетчатки, калия и фолиевой кислоты), бананы (богаты калием, витамином С, витамином B6, клетчаткой), груши (источником витамина C, калия, клетчатки), ягоды (черника, малина и клубника богаты антиоксидантами и витаминами C и К), киви (богаты витамином C, клетчаткой и калием), апельсины (содержат много витамина C и клетчатки, фолиевую кислоту и калий). Однако, некоторые фрукты могут быть опасными для птиц, поэтому необходимо быть осторожным. Например, абрикосы, персики, сливы и черешня могут содержать косточки, которые могут быть опасны для птиц. Кроме того, некоторые фрукты могут быть кислыми и вызывать проблемы с желудком у птиц.", "/images/ingredients/fruits.jpg", "Фрукты" },
                    { 19, "Насекомые содержат много важных питательных веществ, таких как белок, жир, витамины и минералы. Чаще всего в кормах используются: сверчки, мухи, мотыльки, черви, тараканы. При выборе насекомых следует обратить внимание на их размер: более крупные птицы могут легко справляться с крупными сверчками, тогда как маленьким птицам лучше подойдут мелкие насекомые, например, мухи.", "/images/ingredients/insects.jpg", "Насекомые" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Role" },
                values: new object[,]
                {
                    { 1, "Administrator" },
                    { 2, "CommonUser" },
                    { 3, "CommonUser" },
                    { 4, "CommonUser" },
                    { 5, "CommonUser" },
                    { 6, "CommonUser" },
                    { 7, "CommonUser" }
                });

            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "Id", "TotalCost", "UserId" },
                values: new object[,]
                {
                    { 1, 0m, 1 },
                    { 2, 0m, 2 },
                    { 3, 0m, 3 },
                    { 4, 0m, 4 },
                    { 5, 0m, 5 },
                    { 6, 0m, 6 },
                    { 7, 0m, 7 }
                });

            migrationBuilder.InsertData(
                table: "Logins",
                columns: new[] { "Id", "Email", "Password", "UserId" },
                values: new object[,]
                {
                    { 1, "admin@email.com", "$2a$11$6I5bYESn6vMK0XdfAJK6tuOUDjIzspjGeFfHo3CFXyvP3nVB7vf3K", 1 },
                    { 2, "user1@email.com", "$2a$11$0hmFMPV6kEte.WppdQ/UGegpaRvUWrs.WmCWTuqDoS6ivsB2myc0u", 2 },
                    { 3, "user2@email.com", "$2a$11$4BigXG3bg1r9LJS9x1couOxIB97bEB43enH8GDsAwxl.txZYHRJXm", 3 },
                    { 4, "user3@email.com", "$2a$11$iStVedBjnofl449rCWoTle7ugFfiUEhwXdflWZTwliBcZlKc5hVJa", 4 },
                    { 5, "user4@email.com", "$2a$11$ZYF3RfrBwjszubyDI7/65.pMUkHt8J2M.wC9GHYQAp3WXbXAi0EFG", 5 },
                    { 6, "user5@email.com", "$2a$11$ZnOjaT/BCINP8hG5ouqH9uvwqeGq93SV521lVK26oeYV0EdcwPDp.", 6 },
                    { 7, "user6@email.com", "$2a$11$UyXkUCm3zWWs/8Yt7uBlqeBp5HTpIvnRzkE2.NtwzWukbJtyYvmme", 7 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "BirdSize", "CategoryId", "Description", "ImageSource", "Manufacturer", "Name", "NutritionType" },
                values: new object[,]
                {
                    { 1, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Travellor", "Переноска для птиц 1", null },
                    { 2, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Vitakraft", "Переноска для птиц 2", null },
                    { 3, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Malinki", "Переноска для птиц 3", null },
                    { 4, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Malinki", "Переноска для птиц 4", null },
                    { 5, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Malinki", "Переноска для птиц 5", null },
                    { 6, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Travellor", "Переноска для птиц 6", null },
                    { 7, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Travellor", "Переноска для птиц 7", null },
                    { 8, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Vitakraft", "Переноска для птиц 8", null },
                    { 9, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Travellor", "Переноска для птиц 9", null },
                    { 10, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Malinki", "Переноска для птиц 10", null },
                    { 11, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Vitakraft", "Переноска для птиц 11", null },
                    { 12, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Vitakraft", "Переноска для птиц 12", null },
                    { 13, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Vitakraft", "Переноска для птиц 13", null },
                    { 14, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Travellor", "Переноска для птиц 14", null },
                    { 15, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Travellor", "Переноска для птиц 15", null },
                    { 16, "Крупный", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/carriers/vitakraft.jpg", "Malinki", "Переноска для птиц 16", null },
                    { 17, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Malinki", "Переноска для птиц 17", null },
                    { 18, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Vitakraft", "Переноска для птиц 18", null },
                    { 19, "Маленький", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/carriers/malinki.jpg", "Travellor", "Переноска для птиц 19", null },
                    { 20, "Средний", 1, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/carriers/travellor.jpeg", "Travellor", "Переноска для птиц 20", null },
                    { 21, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №1", "Плотоядные" },
                    { 22, "Средний", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/food/padovan.jpg", "Padovan", "Корм для средних птиц №2", "Всеядные" },
                    { 23, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/prestige.jpeg", "Prestige", "Корм для маленьких птиц №3", "Растительноядные" },
                    { 24, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №4", "Насекомоядные" },
                    { 25, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №5", "Плотоядные" },
                    { 26, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/prestige.jpeg", "Prestige", "Корм для маленьких птиц №6", "Всеядные" },
                    { 27, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №7", "Растительноядные" },
                    { 28, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №8", "Насекомоядные" },
                    { 29, "Средний", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/food/padovan.jpg", "Padovan", "Корм для средних птиц №9", "Плотоядные" },
                    { 30, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №10", "Всеядные" },
                    { 31, "Средний", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/food/padovan.jpg", "Padovan", "Корм для средних птиц №11", "Растительноядные" },
                    { 32, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №12", "Насекомоядные" },
                    { 33, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №13", "Плотоядные" },
                    { 34, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №14", "Всеядные" },
                    { 35, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №15", "Растительноядные" },
                    { 36, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №16", "Насекомоядные" },
                    { 37, "Средний", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Средний", "/images/catalog/food/padovan.jpg", "Padovan", "Корм для средних птиц №17", "Плотоядные" },
                    { 38, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №18", "Всеядные" },
                    { 39, "Крупный", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Крупный", "/images/catalog/food/rio.jpg", "Rio", "Корм для крупных птиц №19", "Растительноядные" },
                    { 40, "Маленький", 2, "Изделие, предназначенное для переноски или перевозки птиц из магазина домой, а также из дома в ветеринарную клинику. Размер птицы: Маленький", "/images/catalog/food/fiory.jpg", "Fiory", "Корм для маленьких птиц №20", "Насекомоядные" },
                    { 41, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №1", null },
                    { 42, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №2", null },
                    { 43, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №3", null },
                    { 44, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №4", null },
                    { 45, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №5", null },
                    { 46, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №6", null },
                    { 47, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №7", null },
                    { 48, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №8", null },
                    { 49, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №9", null },
                    { 50, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №10", null },
                    { 51, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №11", null },
                    { 52, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №12", null },
                    { 53, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №13", null },
                    { 54, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №14", null },
                    { 55, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №15", null },
                    { 56, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/doradowood.jpg", "DoradoWood", "Кормушка DoradoWood для птиц уличная  №16", null },
                    { 57, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №17", null },
                    { 58, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/darell.jpg", "Darell", "Кормушка Darell для птиц уличная  №18", null },
                    { 59, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №19", null },
                    { 60, null, 3, "Зима – трудное время для птиц. Нашим маленьким пернатым друзьям часто приходится бороться за выживание. Наши кормушки – это не только птичья столовая, но и украшение сада или балкона. Цветовые решения наших кормушек приближенны к естественным, поэтому птицы станут частыми гостями \"застолья\"!", "/images/catalog/feeders/trixie.jpg", "Trixie", "Кормушка Trixie для птиц уличная  №20", null },
                    { 61, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №1", null },
                    { 62, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №2", null },
                    { 63, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №3", null },
                    { 64, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №4", null },
                    { 65, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №5", null },
                    { 66, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №6", null },
                    { 67, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №7", null },
                    { 68, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №8", null },
                    { 69, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №9", null },
                    { 70, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №10", null },
                    { 71, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №11", null },
                    { 72, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №12", null },
                    { 73, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №13", null },
                    { 74, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №14", null },
                    { 75, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №15", null },
                    { 76, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №16", null },
                    { 77, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №17", null },
                    { 78, "Средний", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Средний", "/images/catalog/goodies/rio.jpg", "Rio", "Лакомство Rio для птиц  №18", null },
                    { 79, "Маленький", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Маленький", "/images/catalog/goodies/vitakraft.jpeg", "Vitakraft", "Лакомство Vitakraft для птиц  №19", null },
                    { 80, "Крупный", 4, "Полезное и функциональное лакомство содержит любимые пернатыми зерна и семена, сбалансировано по микро- и макроэлементам и обогащено витаминами, необходимыми для правильного развития пернатых питомцев. Размер птиц: Крупный", "/images/catalog/goodies/triol.jpg", "Triol", "Лакомство Triol для птиц  №20", null },
                    { 81, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №1", null },
                    { 82, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №2", null },
                    { 83, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №3", null },
                    { 84, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №4", null },
                    { 85, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №5", null },
                    { 86, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №6", null },
                    { 87, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №7", null },
                    { 88, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №8", null },
                    { 89, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №9", null },
                    { 90, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №10", null },
                    { 91, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №11", null },
                    { 92, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №12", null },
                    { 93, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №13", null },
                    { 94, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №14", null },
                    { 95, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №15", null },
                    { 96, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №16", null },
                    { 97, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №17", null },
                    { 98, null, 5, "Спрей представляет собой бесцветную жидкость под давлением (аэрозоль) с запахом чеснока. Против выдергивания перьев у попугаев, в том числе длиннохвостых, и других тропических и певчих птиц при стрессе, пищеварительных расстройствах и прочих нарушениях поведения.", "/images/catalog/careproducts/beaphar.jpg", "Beaphar", "Спрей для птиц Beaphar Papick Spray против выдёргивания перьев №18", null },
                    { 99, null, 5, "Капли против паразитов, для птиц, 50мл.", "/images/catalog/careproducts/baka.jpg", "BAKA", "Капли против паразитов BAKA №19", null },
                    { 100, null, 5, "Спрей на основе безвредных компонентов для человека и птицы. Спрей деликатно очищает, смягчает, питает, успокаивает и оздоравливает перья и кожу птицы. Регулярное применение усиливает яркость оперения, помогает восстановить естественный защитный барьер, облегчает период линьки, предотвращает чрезмерную линьку и поддерживает птицу в нормальном физиологическом состоянии.", "/images/catalog/careproducts/doctoranimal.jpg", "Doctor Animal", "Спрей \"Doctor Animal\" для ухода за оперением птиц №20", null },
                    { 101, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №1", null },
                    { 102, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №2", null },
                    { 103, "Крупный", 6, "Клетка для птиц Ferplast станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Крупный и менее.", "/images/catalog/cells/ferplast.jpg", "Ferplast", "Клетка для птиц Ferplast №3", null },
                    { 104, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №4", null },
                    { 105, "Крупный", 6, "Клетка для птиц Ferplast станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Крупный и менее.", "/images/catalog/cells/ferplast.jpg", "Ferplast", "Клетка для птиц Ferplast №5", null },
                    { 106, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №6", null },
                    { 107, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №7", null },
                    { 108, "Средний", 6, "Клетка для птиц Sky Rainforest станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Средний и менее.", "/images/catalog/cells/skyrainforest.jpg", "Sky Rainforest", "Клетка для птиц Sky Rainforest №8", null },
                    { 109, "Крупный", 6, "Клетка для птиц Ferplast станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Крупный и менее.", "/images/catalog/cells/ferplast.jpg", "Ferplast", "Клетка для птиц Ferplast №9", null },
                    { 110, "Крупный", 6, "Клетка для птиц Ferplast станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Крупный и менее.", "/images/catalog/cells/ferplast.jpg", "Ferplast", "Клетка для птиц Ferplast №10", null },
                    { 111, "Крупный", 6, "Клетка для птиц Ferplast станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Крупный и менее.", "/images/catalog/cells/ferplast.jpg", "Ferplast", "Клетка для птиц Ferplast №11", null },
                    { 112, "Крупный", 6, "Клетка для птиц Ferplast станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Крупный и менее.", "/images/catalog/cells/ferplast.jpg", "Ferplast", "Клетка для птиц Ferplast №12", null },
                    { 113, "Крупный", 6, "Клетка для птиц Ferplast станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Крупный и менее.", "/images/catalog/cells/ferplast.jpg", "Ferplast", "Клетка для птиц Ferplast №13", null },
                    { 114, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №14", null },
                    { 115, "Крупный", 6, "Клетка для птиц Ferplast станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Крупный и менее.", "/images/catalog/cells/ferplast.jpg", "Ferplast", "Клетка для птиц Ferplast №15", null },
                    { 116, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №16", null },
                    { 117, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №17", null },
                    { 118, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №18", null },
                    { 119, "Маленький", 6, "Клетка для птиц Vision станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Маленький.", "/images/catalog/cells/vision.jpg", "Vision", "Клетка для птиц Vision №19", null },
                    { 120, "Крупный", 6, "Клетка для птиц Ferplast станет великолепным домом для вашего пернатого. Просторное жилище придётся по нраву питомцу. Надёжная клетка выполнена из качественного материала, который не впитывает запах и влагу, легко моется и годами сохраняет свой внешний вид. Размер птиц: Крупный и менее.", "/images/catalog/cells/ferplast.jpg", "Ferplast", "Клетка для птиц Ferplast №20", null },
                    { 121, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №1", null },
                    { 122, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №2", null },
                    { 123, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №3", null },
                    { 124, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №4", null },
                    { 125, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №5", null },
                    { 126, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №6", null },
                    { 127, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №7", null },
                    { 128, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №8", null },
                    { 129, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №9", null },
                    { 130, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №10", null },
                    { 131, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №11", null },
                    { 132, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №12", null },
                    { 133, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №13", null },
                    { 134, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №14", null },
                    { 135, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №15", null },
                    { 136, "Средний", 7, "Забавная игрушка для птиц. Размер птиц: Средний.", "/images/catalog/toys/ferplast.jpg", "Ferplast", "Игрушка для птиц Ferplast №16", null },
                    { 137, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №17", null },
                    { 138, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №18", null },
                    { 139, "Маленький", 7, "Забавная игрушка для птиц. Размер птиц: Маленький.", "/images/catalog/toys/triol.jpg", "Triol", "Игрушка для птиц Triol №19", null },
                    { 140, "Крупный", 7, "Забавная игрушка для птиц. Размер птиц: Крупный.", "/images/catalog/toys/doradowood.jpg", "DoradoWood", "Игрушка для птиц DoradoWood №20", null },
                    { 141, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №1", null },
                    { 142, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №2", null },
                    { 143, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №3", null },
                    { 144, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №4", null },
                    { 145, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №5", null },
                    { 146, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №6", null },
                    { 147, null, 8, "Песочные подстилки идеально подойдут для поддержания чистоты клетки вашей птички. Морской песок тщательно обработан и не содержит вредных бактерий и грязи, а приятный запах лимона наполняет пространство клетки свежестью.", "/images/catalog/fillers/fiory.jpg", "Fiory", "Песок для птиц Grit Mint Fiory №7", null },
                    { 148, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №8", null },
                    { 149, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №9", null },
                    { 150, null, 8, "Песочные подстилки идеально подойдут для поддержания чистоты клетки вашей птички. Морской песок тщательно обработан и не содержит вредных бактерий и грязи, а приятный запах лимона наполняет пространство клетки свежестью.", "/images/catalog/fillers/fiory.jpg", "Fiory", "Песок для птиц Grit Mint Fiory №10", null },
                    { 151, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №11", null },
                    { 152, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №12", null },
                    { 153, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №13", null },
                    { 154, null, 8, "Песочные подстилки идеально подойдут для поддержания чистоты клетки вашей птички. Морской песок тщательно обработан и не содержит вредных бактерий и грязи, а приятный запах лимона наполняет пространство клетки свежестью.", "/images/catalog/fillers/fiory.jpg", "Fiory", "Песок для птиц Grit Mint Fiory №14", null },
                    { 155, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №15", null },
                    { 156, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №16", null },
                    { 157, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №17", null },
                    { 158, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №18", null },
                    { 159, null, 8, "Padovan Natural Sand - это наполнитель для птичьих клеток. Очищенные от пыли гранулы кварца насыпаются на дно птичьей клетки для обеспечения лучшей гигиены и облегчения чистки дна.", "/images/catalog/fillers/padovan.jpg", "Padovan", "Наполнитель для дна птичьих клеток PADOVAN Natural Sand №19", null },
                    { 160, null, 8, "Наполнитель Гипоаллергенный изготовлен на основе стружки лиственных пород древесины. Эти опилки изготавливаются из липы или березы на новейшем оборудовании.", "/images/catalog/fillers/vitaline.jpg", "Vitaline", "Опилки гипоаллергенные из лиственных пород древесины Vitaline №20", null },
                    { 161, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №1", null },
                    { 162, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №2", null },
                    { 163, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №3", null },
                    { 164, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №4", null },
                    { 165, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №5", null },
                    { 166, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №6", null },
                    { 167, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №7", null },
                    { 168, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №8", null },
                    { 169, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №9", null },
                    { 170, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №10", null },
                    { 171, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №11", null },
                    { 172, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №12", null },
                    { 173, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №13", null },
                    { 174, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №14", null },
                    { 175, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №15", null },
                    { 176, null, 9, "Деревянные жердочки для клетки с пластиковыми наконечниками. Две штуки длиной 45 см диаметром 10 мм, две штуки длиной 45 см диаметром 12 мм.", "/images/catalog/accessories/trixie.jpg", "Trixie", "Жердочки для клетки Trixie №16", null },
                    { 177, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №17", null },
                    { 178, null, 9, "Качели для Вашего питомца. Изготовлены из древесины.", "/images/catalog/accessories/crystal.jpg", "Crystal", "Качели Crystal №18", null },
                    { 179, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №19", null },
                    { 180, null, 9, "При помощи специального крепления изделие легко и надежно крепится как снаружи, так и внутри клетки. Такая поилка - идеальный вариант для клеток с вертикальными прутьями, при этом расстояние между ними не должно превышать 15 мм.", "/images/catalog/accessories/baka.jpg", "BAKA", "Поилка для птиц ВАКА №20", null }
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "Id", "UserAddress", "UserId", "UserName", "UserTelephone" },
                values: new object[,]
                {
                    { 1, null, 1, "Администратор", null },
                    { 2, "г. Москва, ул. Пользователя 1", 2, "Пользователь 1", "+7 (926) 111-11-11" },
                    { 3, "г. Москва, ул. Пользователя 2", 3, "Пользователь 2", "+7 (926) 222-22-22" },
                    { 4, "г. Москва, ул. Пользователя 3", 4, "Пользователь 3", "+7 (926) 333-33-33" },
                    { 5, "г. Москва, ул. Пользователя 4", 5, "Пользователь 4", "+7 (926) 444-44-44" },
                    { 6, "г. Москва, ул. Пользователя 5", 6, "Пользователь 5", "+7 (926) 555-55-55" },
                    { 7, "г. Москва, ул. Пользователя 6", 7, "Пользователь 6", "+7 (926) 666-66-66" }
                });

            migrationBuilder.InsertData(
                table: "ProductIngredients",
                columns: new[] { "IngredientId", "ProductId", "Id" },
                values: new object[,]
                {
                    { 19, 21, 1 },
                    { 3, 22, 36 },
                    { 4, 22, 37 },
                    { 5, 22, 38 },
                    { 6, 22, 39 },
                    { 19, 22, 11 },
                    { 1, 23, 16 },
                    { 2, 23, 17 },
                    { 3, 23, 18 },
                    { 4, 23, 19 },
                    { 19, 24, 2 },
                    { 19, 25, 3 },
                    { 7, 26, 40 },
                    { 8, 26, 41 },
                    { 9, 26, 42 },
                    { 10, 26, 43 },
                    { 19, 26, 12 },
                    { 5, 27, 20 },
                    { 6, 27, 21 },
                    { 7, 27, 22 },
                    { 8, 27, 23 },
                    { 19, 28, 4 },
                    { 19, 29, 5 },
                    { 11, 30, 44 },
                    { 12, 30, 45 },
                    { 13, 30, 46 },
                    { 14, 30, 47 },
                    { 19, 30, 13 },
                    { 9, 31, 24 },
                    { 10, 31, 25 },
                    { 11, 31, 26 },
                    { 12, 31, 27 },
                    { 19, 32, 6 },
                    { 19, 33, 7 },
                    { 15, 34, 48 },
                    { 16, 34, 49 },
                    { 17, 34, 50 },
                    { 18, 34, 51 },
                    { 19, 34, 14 },
                    { 13, 35, 28 },
                    { 14, 35, 29 },
                    { 15, 35, 30 },
                    { 16, 35, 31 },
                    { 19, 36, 8 },
                    { 19, 37, 9 },
                    { 1, 38, 52 },
                    { 2, 38, 53 },
                    { 3, 38, 54 },
                    { 4, 38, 55 },
                    { 19, 38, 15 },
                    { 1, 39, 34 },
                    { 2, 39, 35 },
                    { 17, 39, 32 },
                    { 18, 39, 33 },
                    { 19, 40, 10 }
                });

            migrationBuilder.InsertData(
                table: "ProductPrices",
                columns: new[] { "Id", "InStock", "Price", "ProductId", "Weight" },
                values: new object[,]
                {
                    { 1, 13, 678m, 1, 0 },
                    { 2, 33, 1089m, 2, 0 },
                    { 3, 62, 905m, 3, 0 },
                    { 4, 29, 1909m, 4, 0 },
                    { 5, 15, 1522m, 5, 0 },
                    { 6, 9, 1378m, 6, 0 },
                    { 7, 27, 545m, 7, 0 },
                    { 8, 91, 889m, 8, 0 },
                    { 9, 76, 1749m, 9, 0 },
                    { 10, 62, 262m, 10, 0 },
                    { 11, 67, 640m, 11, 0 },
                    { 12, 42, 958m, 12, 0 },
                    { 13, 91, 578m, 13, 0 },
                    { 14, 79, 693m, 14, 0 },
                    { 15, 54, 1265m, 15, 0 },
                    { 16, 31, 1064m, 16, 0 },
                    { 17, 5, 1550m, 17, 0 },
                    { 18, 22, 510m, 18, 0 },
                    { 19, 38, 1469m, 19, 0 },
                    { 20, 23, 718m, 20, 0 },
                    { 21, 60, 378m, 21, 200 },
                    { 22, 38, 514m, 21, 400 },
                    { 23, 73, 822m, 21, 800 },
                    { 24, 37, 1405m, 21, 1000 },
                    { 25, 39, 207m, 22, 200 },
                    { 26, 46, 523m, 22, 400 },
                    { 27, 95, 944m, 22, 800 },
                    { 28, 54, 1478m, 22, 1000 },
                    { 29, 93, 250m, 23, 200 },
                    { 30, 52, 416m, 23, 400 },
                    { 31, 64, 981m, 23, 800 },
                    { 32, 20, 1020m, 23, 1000 },
                    { 33, 80, 338m, 24, 200 },
                    { 34, 7, 442m, 24, 400 },
                    { 35, 82, 830m, 24, 800 },
                    { 36, 12, 1436m, 24, 1000 },
                    { 37, 77, 269m, 25, 200 },
                    { 38, 90, 497m, 25, 400 },
                    { 39, 4, 990m, 25, 800 },
                    { 40, 66, 1110m, 25, 1000 },
                    { 41, 28, 348m, 26, 200 },
                    { 42, 81, 496m, 26, 400 },
                    { 43, 84, 926m, 26, 800 },
                    { 44, 53, 1084m, 26, 1000 },
                    { 45, 44, 204m, 27, 200 },
                    { 46, 6, 414m, 27, 400 },
                    { 47, 53, 911m, 27, 800 },
                    { 48, 73, 1448m, 27, 1000 },
                    { 49, 26, 304m, 28, 200 },
                    { 50, 43, 495m, 28, 400 },
                    { 51, 82, 878m, 28, 800 },
                    { 52, 94, 1311m, 28, 1000 },
                    { 53, 0, 285m, 29, 200 },
                    { 54, 18, 481m, 29, 400 },
                    { 55, 24, 890m, 29, 800 },
                    { 56, 39, 1026m, 29, 1000 },
                    { 57, 87, 225m, 30, 200 },
                    { 58, 32, 539m, 30, 400 },
                    { 59, 12, 883m, 30, 800 },
                    { 60, 56, 1180m, 30, 1000 },
                    { 61, 82, 385m, 31, 200 },
                    { 62, 72, 517m, 31, 400 },
                    { 63, 24, 859m, 31, 800 },
                    { 64, 45, 1323m, 31, 1000 },
                    { 65, 77, 333m, 32, 200 },
                    { 66, 25, 570m, 32, 400 },
                    { 67, 58, 891m, 32, 800 },
                    { 68, 94, 1220m, 32, 1000 },
                    { 69, 20, 278m, 33, 200 },
                    { 70, 53, 409m, 33, 400 },
                    { 71, 98, 919m, 33, 800 },
                    { 72, 20, 1078m, 33, 1000 },
                    { 73, 4, 299m, 34, 200 },
                    { 74, 74, 577m, 34, 400 },
                    { 75, 56, 928m, 34, 800 },
                    { 76, 63, 1359m, 34, 1000 },
                    { 77, 65, 365m, 35, 200 },
                    { 78, 88, 410m, 35, 400 },
                    { 79, 2, 999m, 35, 800 },
                    { 80, 2, 1264m, 35, 1000 },
                    { 81, 69, 247m, 36, 200 },
                    { 82, 28, 487m, 36, 400 },
                    { 83, 98, 839m, 36, 800 },
                    { 84, 40, 1197m, 36, 1000 },
                    { 85, 57, 273m, 37, 200 },
                    { 86, 65, 450m, 37, 400 },
                    { 87, 40, 947m, 37, 800 },
                    { 88, 73, 1273m, 37, 1000 },
                    { 89, 46, 322m, 38, 200 },
                    { 90, 64, 543m, 38, 400 },
                    { 91, 88, 881m, 38, 800 },
                    { 92, 17, 1215m, 38, 1000 },
                    { 93, 46, 244m, 39, 200 },
                    { 94, 86, 473m, 39, 400 },
                    { 95, 21, 823m, 39, 800 },
                    { 96, 13, 1426m, 39, 1000 },
                    { 97, 27, 261m, 40, 200 },
                    { 98, 54, 566m, 40, 400 },
                    { 99, 52, 884m, 40, 800 },
                    { 100, 47, 1168m, 40, 1000 },
                    { 101, 94, 246m, 41, 0 },
                    { 102, 34, 282m, 42, 0 },
                    { 103, 45, 1947m, 43, 0 },
                    { 104, 18, 1135m, 44, 0 },
                    { 105, 47, 576m, 45, 0 },
                    { 106, 61, 489m, 46, 0 },
                    { 107, 97, 1318m, 47, 0 },
                    { 108, 44, 1501m, 48, 0 },
                    { 109, 8, 426m, 49, 0 },
                    { 110, 14, 1946m, 50, 0 },
                    { 111, 39, 583m, 51, 0 },
                    { 112, 62, 1171m, 52, 0 },
                    { 113, 98, 732m, 53, 0 },
                    { 114, 52, 1508m, 54, 0 },
                    { 115, 53, 1882m, 55, 0 },
                    { 116, 28, 1592m, 56, 0 },
                    { 117, 37, 762m, 57, 0 },
                    { 118, 56, 1137m, 58, 0 },
                    { 119, 97, 1737m, 59, 0 },
                    { 120, 56, 901m, 60, 0 },
                    { 121, 49, 299m, 61, 0 },
                    { 122, 63, 1657m, 62, 0 },
                    { 123, 86, 1301m, 63, 0 },
                    { 124, 18, 671m, 64, 0 },
                    { 125, 88, 1577m, 65, 0 },
                    { 126, 34, 1793m, 66, 0 },
                    { 127, 87, 1165m, 67, 0 },
                    { 128, 24, 1567m, 68, 0 },
                    { 129, 22, 1463m, 69, 0 },
                    { 130, 6, 1002m, 70, 0 },
                    { 131, 18, 334m, 71, 0 },
                    { 132, 1, 870m, 72, 0 },
                    { 133, 55, 975m, 73, 0 },
                    { 134, 35, 300m, 74, 0 },
                    { 135, 4, 869m, 75, 0 },
                    { 136, 46, 1563m, 76, 0 },
                    { 137, 99, 485m, 77, 0 },
                    { 138, 8, 1520m, 78, 0 },
                    { 139, 5, 817m, 79, 0 },
                    { 140, 29, 1512m, 80, 0 },
                    { 141, 14, 1772m, 81, 0 },
                    { 142, 60, 1440m, 82, 0 },
                    { 143, 18, 1938m, 83, 0 },
                    { 144, 34, 1610m, 84, 0 },
                    { 145, 78, 996m, 85, 0 },
                    { 146, 58, 1824m, 86, 0 },
                    { 147, 36, 1135m, 87, 0 },
                    { 148, 80, 359m, 88, 0 },
                    { 149, 66, 1620m, 89, 0 },
                    { 150, 80, 1919m, 90, 0 },
                    { 151, 2, 1807m, 91, 0 },
                    { 152, 36, 1216m, 92, 0 },
                    { 153, 66, 1111m, 93, 0 },
                    { 154, 46, 806m, 94, 0 },
                    { 155, 99, 340m, 95, 0 },
                    { 156, 84, 519m, 96, 0 },
                    { 157, 84, 1634m, 97, 0 },
                    { 158, 24, 1943m, 98, 0 },
                    { 159, 43, 239m, 99, 0 },
                    { 160, 45, 211m, 100, 0 },
                    { 161, 81, 835m, 101, 0 },
                    { 162, 22, 859m, 102, 0 },
                    { 163, 10, 298m, 103, 0 },
                    { 164, 53, 1867m, 104, 0 },
                    { 165, 39, 600m, 105, 0 },
                    { 166, 5, 1952m, 106, 0 },
                    { 167, 35, 234m, 107, 0 },
                    { 168, 26, 833m, 108, 0 },
                    { 169, 18, 1261m, 109, 0 },
                    { 170, 98, 1977m, 110, 0 },
                    { 171, 77, 1233m, 111, 0 },
                    { 172, 86, 1463m, 112, 0 },
                    { 173, 42, 1234m, 113, 0 },
                    { 174, 26, 1611m, 114, 0 },
                    { 175, 42, 449m, 115, 0 },
                    { 176, 30, 1863m, 116, 0 },
                    { 177, 39, 858m, 117, 0 },
                    { 178, 89, 1592m, 118, 0 },
                    { 179, 70, 950m, 119, 0 },
                    { 180, 65, 1214m, 120, 0 },
                    { 181, 43, 1272m, 121, 0 },
                    { 182, 39, 1414m, 122, 0 },
                    { 183, 44, 558m, 123, 0 },
                    { 184, 24, 250m, 124, 0 },
                    { 185, 24, 731m, 125, 0 },
                    { 186, 54, 946m, 126, 0 },
                    { 187, 52, 1459m, 127, 0 },
                    { 188, 71, 618m, 128, 0 },
                    { 189, 60, 1344m, 129, 0 },
                    { 190, 49, 666m, 130, 0 },
                    { 191, 98, 843m, 131, 0 },
                    { 192, 79, 705m, 132, 0 },
                    { 193, 82, 1384m, 133, 0 },
                    { 194, 87, 507m, 134, 0 },
                    { 195, 17, 617m, 135, 0 },
                    { 196, 78, 1647m, 136, 0 },
                    { 197, 3, 1177m, 137, 0 },
                    { 198, 38, 208m, 138, 0 },
                    { 199, 53, 1853m, 139, 0 },
                    { 200, 88, 306m, 140, 0 },
                    { 201, 6, 567m, 141, 0 },
                    { 202, 2, 1185m, 142, 0 },
                    { 203, 7, 1564m, 143, 0 },
                    { 204, 40, 996m, 144, 0 },
                    { 205, 41, 737m, 145, 0 },
                    { 206, 5, 1263m, 146, 0 },
                    { 207, 9, 1242m, 147, 0 },
                    { 208, 59, 1150m, 148, 0 },
                    { 209, 45, 1075m, 149, 0 },
                    { 210, 91, 1707m, 150, 0 },
                    { 211, 93, 1308m, 151, 0 },
                    { 212, 85, 1407m, 152, 0 },
                    { 213, 34, 1713m, 153, 0 },
                    { 214, 70, 1000m, 154, 0 },
                    { 215, 41, 1627m, 155, 0 },
                    { 216, 18, 1388m, 156, 0 },
                    { 217, 59, 800m, 157, 0 },
                    { 218, 84, 857m, 158, 0 },
                    { 219, 94, 1394m, 159, 0 },
                    { 220, 26, 1727m, 160, 0 },
                    { 221, 15, 1987m, 161, 0 },
                    { 222, 42, 1686m, 162, 0 },
                    { 223, 79, 1150m, 163, 0 },
                    { 224, 67, 1445m, 164, 0 },
                    { 225, 95, 1955m, 165, 0 },
                    { 226, 69, 594m, 166, 0 },
                    { 227, 13, 1901m, 167, 0 },
                    { 228, 11, 848m, 168, 0 },
                    { 229, 44, 267m, 169, 0 },
                    { 230, 43, 448m, 170, 0 },
                    { 231, 85, 1751m, 171, 0 },
                    { 232, 13, 1174m, 172, 0 },
                    { 233, 56, 1329m, 173, 0 },
                    { 234, 81, 427m, 174, 0 },
                    { 235, 31, 851m, 175, 0 },
                    { 236, 34, 755m, 176, 0 },
                    { 237, 50, 1804m, 177, 0 },
                    { 238, 56, 778m, 178, 0 },
                    { 239, 77, 1505m, 179, 0 },
                    { 240, 41, 638m, 180, 0 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ProductId", "UserId", "Comment", "Id", "ReviewDate" },
                values: new object[,]
                {
                    { 4, 4, "Отличное качество, синицы оценили!", 48, new DateTime(2021, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 5, "Отличное качество, синицы оценили!", 20, new DateTime(2019, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 4, "Производителю рекомендовал бы применять более эластичный материал.", 5, new DateTime(2018, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 4, "Производителю рекомендовал бы применять более эластичный материал.", 18, new DateTime(2019, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 4, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 29, new DateTime(2019, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 6, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 45, new DateTime(2019, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 5, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 15, new DateTime(2017, 6, 14, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, 6, "Производителю рекомендовал бы применять более эластичный материал.", 13, new DateTime(2019, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, 7, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 34, new DateTime(2017, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 22, 4, "Отличное качество, синицы оценили!", 21, new DateTime(2021, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, 2, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 31, new DateTime(2020, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 30, 4, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 14, new DateTime(2017, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 31, 3, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 22, new DateTime(2021, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 38, 4, "Отличное качество, синицы оценили!", 30, new DateTime(2019, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 40, 6, "Производителю рекомендовал бы применять более эластичный материал.", 3, new DateTime(2019, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 41, 6, "Производителю рекомендовал бы применять более эластичный материал.", 49, new DateTime(2018, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 43, 4, "Производителю рекомендовал бы применять более эластичный материал.", 4, new DateTime(2019, 5, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 49, 7, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 38, new DateTime(2017, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 53, 4, "Отличное качество, синицы оценили!", 23, new DateTime(2020, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 58, 5, "Отличное качество, синицы оценили!", 25, new DateTime(2020, 11, 6, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 66, 6, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 19, new DateTime(2021, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 69, 7, "Отличное качество, синицы оценили!", 47, new DateTime(2018, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 72, 2, "Отличное качество, синицы оценили!", 37, new DateTime(2017, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 73, 5, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 44, new DateTime(2017, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 77, 7, "Отличное качество, синицы оценили!", 28, new DateTime(2019, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 83, 4, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 9, new DateTime(2019, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 84, 2, "Отличное качество, синицы оценили!", 50, new DateTime(2020, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 102, 5, "Производителю рекомендовал бы применять более эластичный материал.", 10, new DateTime(2018, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 104, 6, "Производителю рекомендовал бы применять более эластичный материал.", 33, new DateTime(2020, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 106, 6, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 27, new DateTime(2020, 6, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 109, 5, "Производителю рекомендовал бы применять более эластичный материал.", 1, new DateTime(2019, 7, 31, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 113, 7, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 12, new DateTime(2020, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 118, 7, "Производителю рекомендовал бы применять более эластичный материал.", 8, new DateTime(2019, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 125, 3, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 7, new DateTime(2018, 8, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 129, 6, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 2, new DateTime(2017, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 137, 4, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 36, new DateTime(2021, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 140, 7, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 26, new DateTime(2019, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 146, 3, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 24, new DateTime(2019, 6, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 148, 6, "Отличное качество, синицы оценили!", 17, new DateTime(2021, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 150, 4, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 32, new DateTime(2020, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 152, 4, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 43, new DateTime(2020, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 155, 7, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 16, new DateTime(2019, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 157, 4, "Производителю рекомендовал бы применять более эластичный материал.", 6, new DateTime(2021, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 164, 6, "Отличное качество, синицы оценили!", 11, new DateTime(2020, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 166, 5, "Производителю рекомендовал бы применять более эластичный материал.", 46, new DateTime(2018, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 166, 7, "Отличное качество, синицы оценили!", 41, new DateTime(2020, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 171, 2, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 40, new DateTime(2018, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 175, 5, "Доверились отзывам и купили, очень надежный производитель. Никаких существенных минусов не заметили, рекомендую!", 35, new DateTime(2019, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 178, 7, "Производителю рекомендовал бы применять более эластичный материал.", 39, new DateTime(2020, 4, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 180, 7, "Прекрасное приобретение, ни сколько не жалею о покупке, птицы довольны.", 42, new DateTime(2021, 12, 29, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "EndDate", "ProductPriceId", "StartDate", "Value" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 4, 9, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 2, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 21 },
                    { 2, new DateTime(2023, 3, 30, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 3, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 12 },
                    { 3, new DateTime(2023, 3, 28, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 23, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 18 },
                    { 4, new DateTime(2023, 3, 22, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 32, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 19 },
                    { 5, new DateTime(2023, 4, 2, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 36, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 17 },
                    { 6, new DateTime(2023, 3, 30, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 42, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 17 },
                    { 7, new DateTime(2023, 3, 28, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 60, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 20 },
                    { 8, new DateTime(2023, 3, 21, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 67, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 15 },
                    { 9, new DateTime(2023, 4, 7, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 71, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 23 },
                    { 10, new DateTime(2023, 3, 22, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 74, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 14 },
                    { 11, new DateTime(2023, 4, 2, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 79, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 19 },
                    { 12, new DateTime(2023, 3, 25, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 83, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 20 },
                    { 13, new DateTime(2023, 3, 26, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 86, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 17 },
                    { 14, new DateTime(2023, 4, 11, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 91, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 24 },
                    { 15, new DateTime(2023, 3, 29, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 96, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 23 },
                    { 16, new DateTime(2023, 3, 31, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 106, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 23 },
                    { 17, new DateTime(2023, 3, 23, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 120, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 18 },
                    { 18, new DateTime(2023, 3, 27, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 121, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 17 },
                    { 19, new DateTime(2023, 3, 21, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 153, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 20 },
                    { 20, new DateTime(2023, 4, 10, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 168, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 21 },
                    { 21, new DateTime(2023, 3, 31, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 175, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 15 },
                    { 22, new DateTime(2023, 4, 5, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 193, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 18 },
                    { 23, new DateTime(2023, 4, 3, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 197, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 10 },
                    { 24, new DateTime(2023, 3, 26, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 236, new DateTime(2023, 3, 13, 0, 37, 14, 313, DateTimeKind.Local).AddTicks(790), 12 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BirdProducts_BirdId",
                table: "BirdProducts",
                column: "BirdId");

            migrationBuilder.CreateIndex(
                name: "IX_BirdProducts_ProductId",
                table: "BirdProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_ProductPriceId",
                table: "Discounts",
                column: "ProductPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_Logins_Email",
                table: "Logins",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Logins_UserId",
                table: "Logins",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductIngredients_IngredientId",
                table: "ProductIngredients",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_OrderId",
                table: "ProductOrders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_ProductId",
                table: "ProductOrders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_ProductId",
                table: "ProductPrices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserTelephone",
                table: "Profiles",
                column: "UserTelephone",
                unique: true,
                filter: "[UserTelephone] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Raitings_UserId",
                table: "Raitings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UserId",
                table: "Reviews",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BirdProducts");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "ProductIngredients");

            migrationBuilder.DropTable(
                name: "ProductOrders");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Raitings");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Birds");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "ProductPrices");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
