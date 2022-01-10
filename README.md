# unity-go-nats

## A basic POC of Bi-directional communication between Go and Unity using NATS

### Setup & Prerequisites

* The code has been tested and setup on Windows
* Install Docker on Windows
* Install Go for Windows
* Install Unity on Windows

### Running the POC

#### Running NATS

`docker run --rm -d -p 4222:4222 -p 6222:6222 -p 8222:8222 --name nats-main nats`
`docker logs nats-main -f`

#### Running the Go Pub/Sub

* Publisher
  
    `go run .\go\example1\natstest-publisher.go`

* Subscriber
  
    `go run .\go\example1\natstest-subscriber.go`

#### Running the Unity Pub/Sub

* Open /unity on UnityHub
* Select `file -> Build and Run` from the to left `file` menu.

