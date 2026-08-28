import pytest


@pytest.mark.parametrize('expected_post_id', [1,2,5],
                         ids=[repr(f'post_id {i}') for i in [1,2,5]])
def test_get_post_by_id_returns_valid_data(api_client, expected_post_id:int):

    status_code, response_data = api_client.get_post_by_id(expected_post_id)

    assert status_code == 200, f"Expected status code 200, but got {status_code}"

    assert response_data.id is not None, "Response JSON does not contain 'id' key"
    assert response_data.title is not None, "Response JSON does not contain 'title' key"
    assert response_data.body is not None, "Response JSON does not contain 'body' key"

    assert response_data.id == expected_post_id , f"Expected ID to be {expected_post_id}, but got {response_data.id}"


def test_get_post_by_unexistent_id_returns_404_not_found(api_client):

    unexistent_post_id = 999
    status_code, response = api_client.get_post_by_id(unexistent_post_id)

    assert status_code == 404, f"Expected status code 404, but got {status_code}"
    assert response == {}, "Response JSON should be empty"


@pytest.mark.parametrize('invalid_post_id', [0,-1],
                         ids=[f'invalid_post_id {i}' for i in [0,-1]])
def test_get_post_by_invalid_id_returns_404_not_found(api_client, invalid_post_id:int):

    status_code, response = api_client.get_post_by_id(invalid_post_id)

    assert status_code == 404, f"Expected status code 404, but got {status_code}"
    assert response == {}, "Response JSON should be empty"


def test_create_new_post_returns_201_created(api_client, post_payload):

    expected_title = post_payload['title']
    expected_body = post_payload['body']

    status_code, response_data = api_client.create_new_post(post_payload)
    assert status_code == 201, f"Expected status code 201, but got {status_code}"
    assert expected_title == response_data.title, f"Expected title should be {expected_title}"
    assert expected_body == response_data.body, f"Expected body should be {expected_body}"
    assert response_data.id is not None, "Response JSON does not contain 'id' key"
