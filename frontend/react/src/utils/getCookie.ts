export function getCookie(name: string): string | null {
  const cookies = document.cookie.split("; ");
  // 可能會有很多個cookie，先做切割
  // 再把各個cookie在切割成key,value 比對key
  for (const cookie of cookies) {
    const [key, value] = cookie.split("=");
    if (key === name) {
      return value;
    }
  }
  return null;
}
