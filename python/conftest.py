import pytest

from clients.posts_client import PostsClient


@pytest.fixture(scope="session")
def base_url():
    print("\n[SETUP] base_url fixture  # scope=session")
    yield "https://jsonplaceholder.typicode.com"
    print("\n[TEARDOWN] base_url fixture  # scope=session")

@pytest.fixture(scope="session")
def api_client(base_url):
    print("\n[SETUP] api_client fixture  # scope=session")
    client = PostsClient(base_url)
    yield client
    client.session.close()
    print("\n[TEARDOWN] api_client fixture  # scope=session")

@pytest.fixture(scope="function")
def post_payload():
    print("\n[SETUP] post_payload fixture  # scope=function")
    payload = {
        "title": "Test Post",
        "body": "Test Body",
        "userId": 1
    }
    yield payload
    print("\n[TEARDOWN] post_payload fixture  # scope=function")
