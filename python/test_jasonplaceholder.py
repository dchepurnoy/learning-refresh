import requests
import pytest


def test_get_post_by_id_returns_valid_data():
    response = get_jsonplaceholder_request(1)
    data = response.json()
    
    assert response.status_code == 200, f"Expected status code 200, but got {response.status_code}"

    assert 'id' in data, "Response JSON does not contain 'id' key"
    assert 'title' in data, "Response JSON does not contain 'title' key"
    assert 'body' in data, "Response JSON does not contain 'body' key"

    assert data['id'] == 1 , f"Expected ID to be 1, but got {data['id']}"
    

def get_jsonplaceholder_request(post_id: int):
    url = f"https://jsonplaceholder.typicode.com/posts/{post_id}"
    response = requests.get(url)
    return response