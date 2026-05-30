import pytest

from clients.PostsClient import PostsClient


@pytest.fixture
def api_client():
    return PostsClient()