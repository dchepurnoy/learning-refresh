import requests

from models.post import Post


class PostsClient:
    BASE_URL = "https://jsonplaceholder.typicode.com/posts"

    @staticmethod
    def get_post_by_id(post_id: int) -> tuple[int, Post]:
        url = f"{PostsClient.BASE_URL}/{post_id}"
        
        response = requests.get(url)
        status_code = response.status_code
        post_model = Post.from_json(response.json())

        return status_code, post_model
