import pytest

from clients.posts_client import PostsClient


@pytest.fixture(scope="session")
def api_client():
    client = PostsClient()
    yield client
    client.session.close()