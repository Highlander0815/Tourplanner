docker stop tour_db 
docker rm tour_db 
docker run -d --name tour_db -e POSTGRES_USER=postgres -e POSTGRES_PASSWORD=postgres -p 5432:5432 postgres

timeout /t 10 /nobreak

docker exec -i tour_db createdb -U postgres tour_db