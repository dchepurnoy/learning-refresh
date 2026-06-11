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


@pytest.mark.parametrize('unexistent_post_id', [0,-1,999],
                         ids=[repr(f'unexistent_post_id {i}') for i in [0,-1,999]])
def test_get_post_by_unexistent_id_returns_404_not_found(api_client, unexistent_post_id:int):

    status_code, response = api_client.get_post_by_id(unexistent_post_id)

    assert status_code == 404, f"Expected status code 404, but got {status_code}"
    assert response == {}, "Response JSON should be empty"

