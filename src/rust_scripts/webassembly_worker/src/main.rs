use hyper::{Body, Request, Response, Server, StatusCode};
use hyper::service::{make_service_fn, service_fn};
use std::convert::Infallible;
use tokio::fs;
use std::path::PathBuf;
use hyper::header::CONTENT_TYPE;
use mime_guess::from_path;

async fn serve_file(path: PathBuf) -> Result<Response<Body>, Infallible> {
    let content = match fs::read(&path).await {
        Ok(content) => content,
        Err(_) => return Ok(Response::builder().status(StatusCode::NOT_FOUND).body(Body::from("404 - Not Found")).unwrap()),
    };

    let mime_type = from_path(&path).first_or_octet_stream().as_ref().to_string();

    Ok(Response::builder()
        .header(CONTENT_TYPE, mime_type)
        .body(Body::from(content)).unwrap())
}

async fn handle_request(req: Request<Body>) -> Result<Response<Body>, Infallible> {
    let path = match req.uri().path().strip_prefix('/') {
        Some(path) => path,
        None => "",
    };

    if path.is_empty() {
        serve_file(PathBuf::from("index.html")).await
    } else {
        serve_file(PathBuf::from(path)).await
    }
}

#[tokio::main]
async fn main() -> Result<(), Box<dyn std::error::Error + Send + Sync>> {
    let make_svc = make_service_fn(|_conn| {
        async { Ok::<_, Infallible>(service_fn(handle_request)) }
    });

    let addr = ([127, 0, 0, 1], 3000).into();

    let server = Server::bind(&addr).serve(make_svc);

    println!("Listening on http://{}", addr);

    server.await?;

    Ok(())
}
