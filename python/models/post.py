from dataclasses import dataclass
from typing import Optional


@dataclass
class Post:
    user_id: Optional[int]
    id: Optional[int]
    title: Optional[str]
    body: Optional[str]

    @classmethod
    def from_json(cls, json_data: dict):
        return cls(
            user_id=json_data.get('userId'),
            id=json_data.get('id'),
            title=json_data.get('title'),
            body=json_data.get('body')
        )