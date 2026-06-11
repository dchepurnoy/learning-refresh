import requests

from models.post import Post


class PostsClient:
    BASE_URL = "https://jsonplaceholder.typicode.com"
    def __init__(self, base_url=BASE_URL):
        self.base_url = base_url
        self.session = requests.Session()

    def get_post_by_id(self, post_id: int) -> tuple[int, Post]:
        url = f"{self.base_url}/posts/{post_id}"
        
        response = self.session.get(url)
        status_code = response.status_code

        if status_code !=200:
            return status_code, response.json()

        post_model = Post.from_json(response.json())

        return status_code, post_model
