
def test_get_post_by_id_returns_valid_data(api_client):
    expected_post_id = 1
    status_code, response_data = api_client.get_post_by_id(expected_post_id)

    assert status_code == 200, f"Expected status code 200, but got {status_code}"

    assert response_data.id, "Response JSON does not contain 'id' key"
    assert response_data.title, "Response JSON does not contain 'title' key"
    assert response_data.body, "Response JSON does not contain 'body' key"

    assert response_data.id == expected_post_id , f"Expected ID to be {expected_post_id}, but got {response_data.id}"
