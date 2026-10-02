export interface Book
{
    id: number;
    title: string;
    author: string;
    publishedDate: string;
}

export interface BookRequest
{
    title: string;
    author: string;
    publishedDate: string;
}