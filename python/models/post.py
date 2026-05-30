from dataclasses import dataclass

@dataclass
class Post:
    userId: int
    id: int
    title: str
    body: str

    @classmethod
    def from_json(cls, json_data: dict):
        return cls(
            userId=json_data.get('userId'),
            id=json_data.get('id'),
            title=json_data.get('title'),
            body=json_data.get('body')
        )