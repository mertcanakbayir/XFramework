Docker Üzerine RabbitMQ Kurulumu

Bilgisayarınızda mevcut değilse önce Docker Desktop indirin: https://www.docker.com/products/docker-desktop/

Docker desktop'a kayıt olun-giriş yapın.

Sağ alttaki terminal'e basıp terminali açtıktan sonra şu komutları yazın:

docker run -d --hostname rabbitmq-host --name rabbitmq \
 -e RABBITMQ_DEFAULT_USER=deneme \
 -e RABBITMQ_DEFAULT_PASS=deneme \
 -p 5672:5672 -p 15672:15672 \
 rabbitmq:3-management

Buradaki default_user ve default_pass bizim rabbitmq'ya giriş için kullanacağımız kullanıcı adı ve şifre.
15672 ise rabbitmq yönetim paneline erişim portu
RabbitMQ giriş paneline docker üzerinden (15672'ye basarak) erişebilir ve girebilirsiniz. 
Mesaj kuyruğunu görmek için rabbitmq üzerinden queues sekmesine bakarak girebilirsiniz.
